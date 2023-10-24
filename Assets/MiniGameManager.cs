using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using System;
using UnityEngine.UI;

public class MiniGameManager : Singleton<MiniGameManager>
{

    [SerializeField] GameObject writingPanel;
    [SerializeField] GameObject petStats;
    [SerializeField] GameObject room;
    [SerializeField] GameObject slider;

    [SerializeField] DialogController lvl0Dialog;

    float currentTime;

    bool minigameStarted = false;

    int currentSign;

    public const int SIGN_TRIANGLE = 0;
    public const int SIGN_HORIZONTAL = 1;
    public const int SIGN_VERTICAL = 2;
    public const int SIGN_U = 3;
    public const int SIGN_SQUARE = 4;

    public void StartMiniGame()
    {
        writingPanel.SetActive(true);
        GameManager.Instance.gameStarted = false;
        Fade(petStats, 1, 0, 0.5f);
        Fade(room, 1, 0, 0.5f);
        Fade(writingPanel, 0, 1, 0.5f);
        currentTime = GameManager.Instance.balance.miniGameDuration;

        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        UIManager.Instance.SetText("TxtTimer", formattedTime);

        minigameStarted = true;

        int currentSign = GameManager.Instance.pet.ShowMiniGameSign();
        slider.GetComponent<Slider>().maxValue = GameManager.Instance.balance.miniGameSliderMax;
        slider.GetComponent<Slider>().value = GameManager.Instance.balance.miniGameSliderMax;
    }

    void Update()
    {
        if (minigameStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
                currentTime = 0;

            if (currentTime == 0)
            {
                EndMiniGame();
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            UIManager.Instance.SetText("TxtTimer", formattedTime);

            slider.GetComponent<Slider>().value -= GameManager.Instance.balance.miniGameTimeDecreaseConstant;

            if (slider.GetComponent<Slider>().value <= 0)
            {
                EndMiniGame(false);
            }
        }
    }

    public void EndMiniGame(bool succeeded = true)
    {
        minigameStarted = false;
        writingPanel.SetActive(false);
        GameManager.Instance.gameStarted = true;

        Fade(petStats, 0, 1, 0.5f);
        Fade(room, 0, 1, 0.5f);
        Fade(writingPanel, 1, 0, 0.5f);

        if (succeeded)
        {
            GameManager.Instance.MiniGamePassed();
        }
        else
        {
            GameManager.Instance.MiniGameFailed();
        }
        GameManager.Instance.pet.HideDialog();
    }

    void Fade(GameObject obj, float from, float to, float duration)
    {
        // LeanTween.cancel(obj);
        LTDescr tween = LeanTween.value(obj, from, to, duration);
        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
    }

    public void Match(string gesture)
    {
        if (gesture == "Triangle" && currentSign == SIGN_TRIANGLE)
        {
            slider.GetComponent<Slider>().value += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
        }
        else if (gesture == "Vertical Line" && currentSign == SIGN_VERTICAL)
        {
            slider.GetComponent<Slider>().value += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
        }
        else if (gesture == "Horizontal Line" && currentSign == SIGN_HORIZONTAL)
        {
            slider.GetComponent<Slider>().value += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
        }
        else if (gesture == "Square" && currentSign == SIGN_SQUARE)
        {
            slider.GetComponent<Slider>().value += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
        }
        else if (gesture == "U" && currentSign == SIGN_U)
        {
            slider.GetComponent<Slider>().value += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
        }
        else
        {
            NotMatch();
        }

        currentSign = GameManager.Instance.pet.ShowMiniGameSign();
    }

    public void NotMatch()
    {
        slider.GetComponent<Slider>().value -= GameManager.Instance.balance.miniGameTimeDecreasePerFailure;
    }

}
