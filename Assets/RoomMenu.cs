using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart()
    {
        if (!GameManager.Instance.gameStarted)
            GameManager.Instance.StartGame();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(0);
    }

}
