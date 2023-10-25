using TurtleGames.Framework.Runtime.Core;
using UnityEngine;

public class RatController : Singleton<RatController>
{

    GameObject mouseObj;

    [SerializeField]
    GameObject spawnRoom0;
    [SerializeField]
    GameObject spawnRoom1;
    [SerializeField]
    GameObject spawnRoom2;
    [SerializeField]
    GameObject spawnRoom3;

    float lastRat;

    void Awake()
    {
        mouseObj = Resources.Load("Mouse") as GameObject;
    }

    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            float cooldown = GameManager.Instance.balance.RatSpawnCooldown - (GameManager.Instance.balance.ratReduceCooldownPerPlant * RoomGarden.Instance.GetPlantsReady());
            if (Time.time - lastRat >= cooldown)
            {
                if (RoomManager.Instance.currentRoom != 2)
                    SpawnRat();
            }
        }
    }

    void SpawnRat()
    {
        GameObject rat = Instantiate(mouseObj, Vector3.zero, Quaternion.identity);

        switch (RoomManager.Instance.currentRoom)
        {
            case 0:
                rat.GetComponent<Rat>().SetPosition(spawnRoom0.transform.position);
                break;
            case 1:
                rat.GetComponent<Rat>().SetPosition(spawnRoom1.transform.position);
                break;
            case 2:
                rat.GetComponent<Rat>().SetPosition(spawnRoom2.transform.position);
                break;
            case 3:
                rat.GetComponent<Rat>().SetPosition(spawnRoom3.transform.position);
                break;
        }

        lastRat = Time.time;
    }

}
