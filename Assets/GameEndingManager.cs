using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TurtleGames.Framework.Runtime.Camera;

public class GameEndingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<CameraController>().FadeInFromColor(1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnReturnClick()
    {
        SceneManager.LoadScene(0);
    }
}
