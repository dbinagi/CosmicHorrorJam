using TurtleGames.Framework.Runtime.Core;
using UnityEngine;

public class RatController : Singleton<RatController>
{

    GameObject mouseObj;

    float lastRat;

    void Awake()
    {
        mouseObj = Resources.Load("Mouse") as GameObject;
    }

    void Update()
    {
        if (Time.time - lastRat >= GameManager.Instance.balance.RatSpawnCooldown)
        {
            SpawnRat();
        }
    }

    void SpawnRat()
    {
        GameObject rat = Instantiate(mouseObj, Vector3.zero, Quaternion.identity);
        rat.transform.position = Camera.main.transform.forward * -0.7f;
        rat.transform.position = rat.transform.position + (Camera.main.transform.right * 3f);
        lastRat = Time.time;
    }

}
