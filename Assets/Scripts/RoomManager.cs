using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using TurtleGames.Framework.Runtime.Audio;

public class RoomManager : Singleton<RoomManager>
{

    public const int ROOM_PET = 0;
    public const int ROOM_CULT = 1;
    public const int ROOM_MENU = 2;
    public const int ROOM_BED = 3;

    public int currentRoom;

    [SerializeField]
    LeanTweenType moveEasing;

    List<GameObject> roomUI = new List<GameObject>();

    bool canMove = true;

    void Start()
    {
        currentRoom = ROOM_MENU;
        roomUI.Add(UIManager.Instance.FindInCanvas("PetRoom"));
        roomUI.Add(UIManager.Instance.FindInCanvas("CultRoom"));
        roomUI.Add(UIManager.Instance.FindInCanvas("MenuRoom"));
        roomUI.Add(UIManager.Instance.FindInCanvas("GardenRoom"));
    }

    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                GoRight();
                MoveToRoom(currentRoom);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                GoLeft();
                MoveToRoom(currentRoom);
            }
        }
    }

    void GoLeft()
    {
        currentRoom -= 1;
        AudioManager.Instance.PlayRandomInGroup("eldritchpet_sfx_uiRoomSwoosh");
        if (currentRoom < 0)
            currentRoom = ROOM_BED;
    }

    void GoRight()
    {
        currentRoom += 1;
        AudioManager.Instance.PlayRandomInGroup("eldritchpet_sfx_uiRoomSwoosh");
        if (currentRoom > 3)
            currentRoom = ROOM_PET;
    }

    public void MoveToRoom(int room)
    {
        currentRoom = room;
        canMove = false;
        float to = room * 90;
        LeanTween.cancel(Camera.main.gameObject);
        LeanTween.rotateY(Camera.main.gameObject, to, 0.7f).setEase(moveEasing).setOnComplete(() => { canMove = true; });

        foreach (GameObject ui in roomUI)
        {
            if (ui == roomUI[room])
            {
                ui.SetActive(true);
            }
            else
            {
                if (ui.GetComponent<CanvasGroup>().alpha > 0)
                {
                    LTDescr tweenOut = LeanTween.value(ui, 1, 0, 0.2f);

                    tweenOut.setOnUpdate((float alpha) =>
                    {
                        ui.GetComponent<CanvasGroup>().alpha = alpha;
                    }).setOnComplete(() => { ui.SetActive(false); });
                }
            }
        }

        LTDescr tween = LeanTween.value(roomUI[room], 0, 1, 0.5f);

        tween.setOnUpdate((float alpha) =>
        {
            roomUI[room].GetComponent<CanvasGroup>().alpha = alpha;
        });

    }

}
