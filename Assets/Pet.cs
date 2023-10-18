using TurtleGames.Framework.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{

    Animator animator;

    [SerializeField] float blinkMinCooldown;
    [SerializeField] float blinkMaxCooldown;

    int currentLevel = 0;
    float currentHunger;

    float blinkCD;
    float lastBlink;

    float hungerCD;
    float lastHunger;

    #region Unity Functions

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        blinkCD = Random.Range(blinkMinCooldown, blinkMaxCooldown);
        hungerCD = Random.Range(GameManager.Instance.balance.hungerMinCooldownLevel0, GameManager.Instance.balance.hungerMaxCooldownLevel0);
        currentHunger = GameManager.Instance.balance.petMaxHunger;
        GameObject sliderHunger = UIManager.Instance.FindInCanvas("SliderHunger");
        sliderHunger.GetComponent<Slider>().maxValue = currentHunger;
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
    }

    #endregion

    #region Public Functions

    public void Feed(float food)
    {
        animator.SetTrigger("Eat");

        currentHunger = Mathf.Clamp(currentHunger+food, 0, GameManager.Instance.balance.petMaxHunger);
        GameObject slider = UIManager.Instance.FindInCanvas("SliderHunger");
        slider.GetComponent<Slider>().value = currentHunger;
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    #endregion

    #region Private Functions

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

        if (currentHunger <= 0){
            GameManager.Instance.DieByFood();
        }
    }

    #endregion

}
