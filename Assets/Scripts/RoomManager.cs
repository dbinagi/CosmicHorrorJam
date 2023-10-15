using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;

public class RoomManager : Singleton<RoomManager>
{


    int currentRoom;

    [SerializeField]
    LeanTweenType moveEasing;

    void Start()
    {
        currentRoom = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            float to = Camera.main.transform.rotation.eulerAngles.y;
            to += 90;

            // LeanTween.cancel(Camera.main.gameObject);
            LeanTween.rotateY(Camera.main.gameObject, to, 1).setEase(moveEasing);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            float to = Camera.main.transform.rotation.eulerAngles.y;
            to -= 90;
            // LeanTween.cancel(Camera.main.gameObject);
            LeanTween.rotateY(Camera.main.gameObject, to, 1).setEase(moveEasing);
        }
    }

}
