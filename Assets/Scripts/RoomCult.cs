using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

public class RoomCult : Singleton<RoomCult>
{


    bool humanSelectorOpen;


    public void OnBuyHumanSelectorClick()
    {
        if (humanSelectorOpen)
        {
            CloseHumanSelector();
        }
        else
        {
            OpenHumanSelector();
        }
    }

    public void OnBuyHumanPickClick()
    {
        GameManager.Instance.BuyHuman(GameManager.Instance.GetHumanCost());
        CloseHumanSelector();
    }


    void OpenHumanSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("HumanSelector");
        obj.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0;

        UIManager.Instance.SetText("TxtHumanCost", "Human $" + GameManager.Instance.GetHumanCost() + " points");
        if (GameManager.Instance.currentCultPoints < GameManager.Instance.GetHumanCost())
        {
            GameObject btnHuman = UIManager.Instance.FindInCanvas("BtnBuyHuman");
            btnHuman.GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject btnHuman = UIManager.Instance.FindInCanvas("BtnBuyHuman");
            btnHuman.GetComponent<Button>().interactable = true;
        }

        LTDescr tween = LeanTween.value(obj, 0, 1, 0.5f);

        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
        humanSelectorOpen = true;
    }

    void CloseHumanSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("HumanSelector");
        LTDescr tween = LeanTween.value(obj, 1, 0, 0.2f);
        tween.setOnUpdate((float alpha) =>
        {
            obj.GetComponent<CanvasGroup>().alpha = alpha;
        });
        humanSelectorOpen = false;
    }

}
