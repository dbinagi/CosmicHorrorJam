using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;

public class RoomPet : Singleton<RoomPet>
{

    public void OnFeedClick()
    {
        OpenFoodSelector();
    }

    public void FeedRat()
    {
        GameManager.Instance.pet.Feed(GameManager.Instance.balance.foodRatValue);
        CloseFoodSelector();
    }

    public void FeedHuman()
    {
        GameManager.Instance.pet.Feed(GameManager.Instance.balance.foodHumanValue);
        CloseFoodSelector();
    }

    public void FeedVegetables()
    {
        GameManager.Instance.pet.Feed(GameManager.Instance.balance.foodVegetablesValue);
        CloseFoodSelector();
    }

    void OpenFoodSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("FoodSelector");
        obj.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0;

        LTDescr tween = LeanTween.value(obj, 0, 1, 0.5f);

        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
    }

    void CloseFoodSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("FoodSelector");
        LTDescr tween = LeanTween.value(obj, 1, 0, 0.2f);
        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
    }

}
