using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Camera;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{

    Animator animator;

    [SerializeField] float blinkMinCooldown;
    [SerializeField] float blinkMaxCooldown;

    [SerializeField]
    GameObject petLvl0;

    [SerializeField]
    GameObject petLvl1;

    [SerializeField]
    GameObject petLvl2;

    [SerializeField]
    List<PoopSlot> poopPositions;

    public int currentLevel = 0;
    float currentHunger;

    float blinkCD;
    float lastBlink;

    float hungerCD;
    float lastHunger;

    float poopCD;
    float lastPoop;
    Stack<int> poopStack = new Stack<int>();

    float currentWellbeing;

    #region Unity Functions

    void Awake()
    {
        animator = petLvl0.GetComponentInChildren<Animator>();
        blinkCD = Random.Range(blinkMinCooldown, blinkMaxCooldown);
        hungerCD = Random.Range(GameManager.Instance.balance.hungerMinCooldownLevel0, GameManager.Instance.balance.hungerMaxCooldownLevel0);
        currentHunger = GameManager.Instance.balance.petMaxHunger;
        GameObject sliderHunger = UIManager.Instance.FindInCanvas("SliderHunger");
        sliderHunger.GetComponent<Slider>().maxValue = currentHunger;

        currentWellbeing = GameManager.Instance.balance.petMaxWellbeing;
        GameObject sliderWellbeing = UIManager.Instance.FindInCanvas("SliderWellbeing");
        sliderWellbeing.GetComponent<Slider>().maxValue = currentHunger;

        poopCD = Random.Range(GameManager.Instance.balance.poopMinCooldownLevel0, GameManager.Instance.balance.poopMinCooldownLevel0);
    }

    void Update()
    {
        if (Time.time - lastBlink >= blinkCD)
        {
            Blink();
        }

        if (Time.time - lastHunger >= hungerCD)
        {
            Hunger();
        }

        if (Time.time - lastPoop >= poopCD)
        {
            Poop();
        }
    }

    #endregion

    #region Public Functions

    public void Feed(float food)
    {
        animator.SetTrigger("Eat");
        poopStack.Push(1);

        currentHunger = Mathf.Clamp(currentHunger + food, 0, GameManager.Instance.balance.petMaxHunger);
        GameObject slider = UIManager.Instance.FindInCanvas("SliderHunger");
        slider.GetComponent<Slider>().value = currentHunger;
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
        Camera.main.GetComponent<CameraController>().FadeOutToColor(0.1f);
        StartCoroutine(ChangeSprite());
    }

    #endregion

    #region Private Functions

    IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(0.2f);

        if (currentLevel == 1)
        {
            petLvl0.SetActive(false);
            petLvl1.SetActive(true);
            animator = petLvl1.GetComponentInChildren<Animator>();
        }
        else if (currentLevel == 2)
        {
            petLvl1.SetActive(false);
            petLvl2.SetActive(true);
            animator = petLvl2.GetComponentInChildren<Animator>();
        }
        Camera.main.GetComponent<CameraController>().FadeInFromColor(0.3f);
    }


    void Blink()
    {
        animator.SetTrigger("Blink");
        blinkCD = Random.Range(blinkMinCooldown, blinkMaxCooldown);
        lastBlink = Time.time;
    }

    void Hunger()
    {
        lastHunger = Time.time;

        float hungerValue = 0;

        switch (currentLevel)
        {
            case 0:
                hungerCD = Random.Range(GameManager.Instance.balance.hungerMinCooldownLevel0, GameManager.Instance.balance.hungerMaxCooldownLevel0);
                hungerValue = Random.Range(GameManager.Instance.balance.hungerLossMinLevel0, GameManager.Instance.balance.hungerLossMaxLevel0);
                currentHunger = Mathf.Clamp(currentHunger - hungerValue, 0, 1000);
                break;
            case 1:
                hungerCD = Random.Range(GameManager.Instance.balance.hungerMinCooldownLevel1, GameManager.Instance.balance.hungerMaxCooldownLevel1);
                hungerValue = Random.Range(GameManager.Instance.balance.hungerLossMinLevel1, GameManager.Instance.balance.hungerLossMaxLevel1);
                currentHunger = Mathf.Clamp(currentHunger - hungerValue, 0, 1000);
                break;
            case 2:
                hungerCD = Random.Range(GameManager.Instance.balance.hungerMinCooldownLevel2, GameManager.Instance.balance.hungerMaxCooldownLevel2);
                hungerValue = Random.Range(GameManager.Instance.balance.hungerLossMinLevel2, GameManager.Instance.balance.hungerLossMaxLevel2);
                currentHunger = Mathf.Clamp(currentHunger - hungerValue, 0, 1000);
                break;
        }
        GameObject slider = UIManager.Instance.FindInCanvas("SliderHunger");
        slider.GetComponent<Slider>().value = currentHunger;

        if (currentHunger <= 0)
        {
            GameManager.Instance.DieByFood();
        }
    }

    void Poop()
    {
        lastPoop = Time.time;

        if (poopStack.Count == 0)
        {
            return;
        }
        int amount = poopStack.Pop();

        bool couldPoop = false;

        switch (currentLevel)
        {
            case 0:
                poopCD = Random.Range(GameManager.Instance.balance.poopMinCooldownLevel0, GameManager.Instance.balance.poopMaxCooldownLevel0);
                RandomizeList(poopPositions);
                foreach (PoopSlot slot in poopPositions)
                {
                    if (slot.isEmpty)
                    {
                        couldPoop = true;
                        slot.Poop();
                        break;
                    }
                }
                if (!couldPoop)
                {
                    GameManager.Instance.CouldNotPoop();
                    currentWellbeing -= Random.Range(GameManager.Instance.balance.wellbeingMinLossPerNotPoopLevel0, GameManager.Instance.balance.wellbeingMaxLossPerNotPoopLevel0);
                    GameObject sliderWellbeing = UIManager.Instance.FindInCanvas("SliderWellbeing");
                    sliderWellbeing.GetComponent<Slider>().value = currentWellbeing;
                }
                break;
            case 1:
                poopCD = Random.Range(GameManager.Instance.balance.poopMinCooldownLevel1, GameManager.Instance.balance.poopMaxCooldownLevel1);
                RandomizeList(poopPositions);
                foreach (PoopSlot slot in poopPositions)
                {
                    if (slot.isEmpty)
                    {
                        couldPoop = true;
                        slot.Poop();
                        break;
                    }
                }
                if (!couldPoop)
                {
                    GameManager.Instance.CouldNotPoop();
                    currentWellbeing -= Random.Range(GameManager.Instance.balance.wellbeingMinLossPerNotPoopLevel1, GameManager.Instance.balance.wellbeingMaxLossPerNotPoopLevel1);
                    GameObject sliderWellbeing = UIManager.Instance.FindInCanvas("SliderWellbeing");
                    sliderWellbeing.GetComponent<Slider>().value = currentWellbeing;
                }
                break;
            case 2:
                poopCD = Random.Range(GameManager.Instance.balance.poopMinCooldownLevel2, GameManager.Instance.balance.poopMaxCooldownLevel2);
                RandomizeList(poopPositions);
                foreach (PoopSlot slot in poopPositions)
                {
                    if (slot.isEmpty)
                    {
                        couldPoop = true;
                        slot.Poop();
                        break;
                    }
                }
                if (!couldPoop)
                {
                    GameManager.Instance.CouldNotPoop();
                    currentWellbeing -= Random.Range(GameManager.Instance.balance.wellbeingMinLossPerNotPoopLevel2, GameManager.Instance.balance.wellbeingMaxLossPerNotPoopLevel2);
                    GameObject sliderWellbeing = UIManager.Instance.FindInCanvas("SliderWellbeing");
                    sliderWellbeing.GetComponent<Slider>().value = currentWellbeing;

                }
                break;
        }
    }

    // Método para mezclar aleatoriamente una lista
    private void RandomizeList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int j = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    #endregion

}
