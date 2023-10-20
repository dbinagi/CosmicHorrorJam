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
    public int currentCultPoints;

    void Start()
    {
        currentCultPoints = 0;
        UIManager.Instance.SetText("TxtCultPoints", "Cult Points: " + currentCultPoints);
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
                }
                PoopSlot poop = hit.Value.transform.GetComponent<PoopSlot>();
                if (poop != null)
                {
                    poop.TakePoop();
                    currentCultPoints += Random.Range(balance.minCultPointsPerPoop, balance.maxCultPointsPerPoop);
                    UIManager.Instance.SetText("TxtCultPoints", "Cult Points: " + currentCultPoints);
                }
            }
        }
    }

    public void DieByFood()
    {
        SceneManager.LoadScene(0);
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

}
