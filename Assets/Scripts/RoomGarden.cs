using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Core;
using UnityEngine;

public class RoomGarden : Singleton<RoomGarden>
{

    public List<Plant> plants;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetPlantsReady()
    {
        int total = 0;
        foreach (Plant plant in plants)
        {
            if (plant.currentStage == Plant.STAGE_READY)
            {
                total++;
            }
        }
        return total;
    }

}
