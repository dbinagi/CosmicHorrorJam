using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine.UI;

public class RoomPet : Singleton<RoomPet>
{

    bool foodSelectorOpen = false;

    public void OnFeedClick()
    {
        if (foodSelectorOpen)
        {
            CloseFoodSelector();
        }
        else
        {
            OpenFoodSelector();
        }
    }

    public void FeedRat()
    {
        GameManager.Instance.pet.Feed(GameManager.Instance.balance.foodRatValue);
        CloseFoodSelector();
        GameManager.Instance.ratAmount -= 1;
    }

    public void FeedHuman()
    {
        GameManager.Instance.pet.Feed(GameManager.Instance.balance.foodHumanValue);
        CloseFoodSelector();
        GameManager.Instance.humanAmount -= 1;
    }

    public void FeedVegetables()
    {
        GameManager.Instance.pet.Feed(GameManager.Instance.balance.foodVegetablesValue);
        CloseFoodSelector();
        GameManager.Instance.vegAmount -= 1;
    }

    void OpenFoodSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("FoodSelector");
        obj.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0;

        UIManager.Instance.SetText("TxtRatAmount", "x" + GameManager.Instance.ratAmount + " rats");
        if (GameManager.Instance.ratAmount <= 0)
        {
            GameObject btnRat = UIManager.Instance.FindInCanvas("BtnFeedRat");
            btnRat.GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject btnRat = UIManager.Instance.FindInCanvas("BtnFeedRat");
            btnRat.GetComponent<Button>().interactable = true;
        }

        UIManager.Instance.SetText("TxtHumanAmount", "x" + GameManager.Instance.humanAmount + " humans");
        if (GameManager.Instance.humanAmount <= 0)
        {
            GameObject btnRat = UIManager.Instance.FindInCanvas("BtnFeedHuman");
            btnRat.GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject btnRat = UIManager.Instance.FindInCanvas("BtnFeedHuman");
            btnRat.GetComponent<Button>().interactable = true;
        }

        UIManager.Instance.SetText("TxtVegetablesAmount", "x" + GameManager.Instance.vegAmount + " vegetables");
        if (GameManager.Instance.vegAmount <= 0)
        {
            GameObject btnRat = UIManager.Instance.FindInCanvas("BtnFeedVegetables");
            btnRat.GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject btnRat = UIManager.Instance.FindInCanvas("BtnFeedVegetables");
            btnRat.GetComponent<Button>().interactable = true;
        }

        LTDescr tween = LeanTween.value(obj, 0, 1, 0.5f);

        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
        foodSelectorOpen = true;
    }

    void CloseFoodSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("FoodSelector");
        LTDescr tween = LeanTween.value(obj, 1, 0, 0.2f);
        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
        foodSelectorOpen = false;
    }

}
