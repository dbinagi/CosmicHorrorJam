using UnityEngine;

public class Rat : MonoBehaviour
{

    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        body.Move(this.transform.position + ((this.transform.right * -1) * Time.deltaTime * GameManager.Instance.GetRatSpeed()), Quaternion.identity);
    }
}
