using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;

public class RoomPet : Singleton<RoomPet>
{


    public void OnFeedClick()
    {

        GameManager.Instance.pet.Feed();

    }

}
