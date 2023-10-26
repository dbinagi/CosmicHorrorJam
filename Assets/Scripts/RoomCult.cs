using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Audio;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

public class RoomCult : Singleton<RoomCult>
{

    [SerializeField]
    GameObject humanBag;

    bool humanSelectorOpen;


    public void OnBuyHumanSelectorClick()
    {
        AudioManager.Instance.PlayOneShot("eldritchpet_sfx_uiClick");
        if (humanSelectorOpen)
        {
            CloseHumanSelector();
        }
        else
        {
            if (!humanBag.activeSelf)
                OpenHumanSelector();
        }
    }

    public void OnBuyHumanPickClick()
    {
        AudioManager.Instance.PlayRandomInGroup("eldritchpet_sfx_buyHuman");
        if (!humanBag.activeSelf)
        {
            GameManager.Instance.BuyHuman(GameManager.Instance.GetHumanCost());
            CloseHumanSelector();
            humanBag.SetActive(true);
        }
    }


    void OpenHumanSelector()
    {
        GameObject obj = UIManager.Instance.FindInCanvas("HumanSelector");
        obj.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0;

        UIManager.Instance.SetText("TxtHumanCost", "Human: " + GameManager.Instance.GetHumanCost() + " tokens");
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
