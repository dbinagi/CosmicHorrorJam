using System.Collections;
using TurtleGames.Framework.Runtime.Audio;
using TurtleGames.Framework.Runtime.Camera;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    public Pet pet;

    [SerializeField]
    public SOBalance balance;

    [SerializeField]
    public int ratAmount;

    [SerializeField]
    public int humanAmount;

    [SerializeField]
    public int vegAmount;

    [SerializeField]
    public int currentCultPoints;

    public int totalHumanPurchases;

    public bool gameStarted;

    void Start()
    {
        gameStarted = false;
        currentCultPoints = 0;
        RefreshCultPoints();
        Camera.main.GetComponent<CameraController>().FadeInFromColor(2);
        AudioManager.Instance.FadeIn("eldrichPet_music_startLoop");
        Sound startLoop = AudioManager.Instance.GetSound("eldrichPet_music_startLoop");
        StartCoroutine(StartEnd(startLoop.source));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit? hit = GetMouseRaycastHit();
            if (hit != null)
            {
                Rat rat = hit.Value.transform.GetComponent<Rat>();
                if (rat != null)
                {
                    ratAmount += 1;
                    Destroy(rat.gameObject);
                    AudioManager.Instance.PlayRandomInGroup("eldritchpet_sfx_ratPickup");
                }
                PoopSlot poop = hit.Value.transform.GetComponent<PoopSlot>();
                if (poop != null)
                {
                    poop.TakePoop();
                    currentCultPoints += Random.Range(balance.minCultPointsPerPoop, balance.maxCultPointsPerPoop);
                    RefreshCultPoints();
                    AudioManager.Instance.PlayOneShot("eldritchpet_sfx_pickupCaca");
                }
                Plant plant = hit.Value.transform.GetComponent<Plant>();
                if (plant != null)
                {
                    if (plant.currentStage == Plant.STAGE_READY)
                    {
                        plant.Take();
                        vegAmount++;
                        AudioManager.Instance.PlayRandomInGroup("eldritchpet_sfx_vegetablePickup");
                    }
                }
                HumanBag humanBag = hit.Value.transform.GetComponent<HumanBag>();
                if (humanBag != null)
                {
                    humanAmount++;
                    humanBag.gameObject.SetActive(false);
                }
            }
        }
    }

    public void DieByFood()
    {
        SceneManager.LoadScene("GameEndingLose");
    }

    public void DieByWellbeing()
    {
        SceneManager.LoadScene("GameEndingLose");
    }

    public float GetRatSpeed()
    {
        if (pet.currentLevel == 0)
        {
            return balance.RatSpeedLevel0;
        }
        else if (pet.currentLevel == 1)
        {
            return balance.RatSpeedLevel1;
        }
        else
        {
            return balance.RatSpeedLevel2;
        }
    }

    RaycastHit? GetMouseRaycastHit()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        LayerMask mask = LayerMask.GetMask("Interactable");

        if (Physics.Raycast(ray, out RaycastHit hitData, 100.0f, mask))
        {
            return hitData;
        }
        return null;
    }

    public void CouldNotPoop()
    {
    }

    public int GetHumanCost()
    {
        return (int)(balance.initialHumanCost + (totalHumanPurchases * balance.costIncreasePerPurchase));
    }

    public void BuyHuman(int cost)
    {
        totalHumanPurchases++;
        currentCultPoints -= cost;
        // humanAmount++;
        RefreshCultPoints();
    }

    public void RefreshCultPoints()
    {
        UIManager.Instance.SetText("TxtCultPoints", "Cult Points: " + currentCultPoints);
    }

    public void StartGame()
    {
        gameStarted = true;
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        UIManager.Instance.FindInCanvas("MenuRoom").SetActive(false);
        RoomManager.Instance.MoveToRoom(RoomManager.ROOM_PET);

        GameObject obj = UIManager.Instance.FindInCanvas("Stats");
        obj.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0;

        LTDescr tween = LeanTween.value(obj, 0, 1, 1.0f);

        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });

        yield return new WaitForSeconds(1);

        UIManager.Instance.FindInCanvas("TutorialRoom").SetActive(true);
    }

    IEnumerator StartEnd(AudioSource source)
    {
        yield return new WaitForSeconds(114.8f);
        AudioManager.Instance.Play("eldritchPet_music_mainLoop", false, 1, 0.5f, true);
    }

    public void GameEndingLevel3Reached()
    {
        StartCoroutine(StartGameEndingLevel3Reached());
    }

    IEnumerator StartGameEndingLevel3Reached()
    {
        Camera.main.GetComponent<CameraController>().FadeOutToColor(1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void MiniGameFailed()
    {
        pet.LossWellbeing(balance.miniGameWellbeingCostForLosing);
    }

    public void MiniGamePassed()
    {
        pet.LossWellbeing(balance.miniGameWellbeingForSuccess * -1);
    }

    public int GetMiniGameTime()
    {
        if (pet.currentLevel == 0)
        {
            return balance.miniGameDurationLvl0;
        }
        else if (pet.currentLevel == 1)
        {
            return balance.miniGameDurationLvl1;
        }
        else
        {
            return balance.miniGameDurationLvl2;
        }
    }

    public int GetMiniGameSuccessNeeded()
    {
        if (pet.currentLevel == 0)
        {
            return balance.miniGameSuccessNeededLvl0;
        }
        else if (pet.currentLevel == 1)
        {
            return balance.miniGameSuccessNeededLvl1;
        }
        else
        {
            return balance.miniGameSuccessNeededLvl2;
        }
    }
}
