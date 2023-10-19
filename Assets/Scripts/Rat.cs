using UnityEngine;

public class Rat : MonoBehaviour
{

    Rigidbody body;
    int roomSpawned;
    float timeSpawned;

    bool rotationSet;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        roomSpawned = RoomManager.Instance.currentRoom;
        timeSpawned = Time.time;
        rotationSet = false;
    }

    void FixedUpdate()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Abs(roomSpawned * 90), 0);
        body.Move(this.transform.position + ((this.transform.right * -1) * Time.deltaTime * GameManager.Instance.GetRatSpeed()), Quaternion.identity);
        if (Time.time - timeSpawned >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPosition(Vector3 pos)
    {
        this.transform.position = pos;
    }
}
