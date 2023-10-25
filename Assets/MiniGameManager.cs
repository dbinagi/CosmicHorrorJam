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

    float currentTime;

    bool minigameStarted = false;

    int currentSign;
    int remainingSuccess;

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

        currentTime = GameManager.Instance.GetMiniGameTime();
        remainingSuccess = GameManager.Instance.GetMiniGameSuccessNeeded();

        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        UIManager.Instance.SetText("TxtTimer", formattedTime);

        minigameStarted = true;

        currentSign = GameManager.Instance.pet.ShowMiniGameSign();
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
                EndMiniGame(false);
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            UIManager.Instance.SetText("TxtTimer", formattedTime);

            UIManager.Instance.SetText("TxtMiniGameRemaining", "Remaining: " + remainingSuccess);

            if (remainingSuccess == 0)
            {
                EndMiniGame();
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
        LeanTween.cancel(obj);
        LTDescr tween = LeanTween.value(obj, from, to, duration);
        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
    }

    public void Match(string gesture)
    {
        if (gesture.ToLower() == "triangle" && currentSign == SIGN_TRIANGLE)
        {
            currentTime += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
            remainingSuccess--;
        }
        else if (gesture.ToLower() == "vertical line" && currentSign == SIGN_VERTICAL)
        {
            currentTime += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
            remainingSuccess--;
        }
        else if (gesture.ToLower() == "horizontal line" && currentSign == SIGN_HORIZONTAL)
        {
            currentTime += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
            remainingSuccess--;
        }
        else if (gesture.ToLower() == "square" && currentSign == SIGN_SQUARE)
        {
            currentTime += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
            remainingSuccess--;
        }
        else if (gesture.ToLower() == "u" && currentSign == SIGN_U)
        {
            currentTime += GameManager.Instance.balance.miniGameTimeIncreasePerSuccess;
            remainingSuccess--;
        }
        else
        {
            NotMatch();
        }

        currentSign = GameManager.Instance.pet.ShowMiniGameSign();
    }

    public void NotMatch()
    {
        currentTime -= GameManager.Instance.balance.miniGameTimeDecreasePerFailure;
    }

}
