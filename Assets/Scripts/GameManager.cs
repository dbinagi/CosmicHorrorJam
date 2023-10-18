using TurtleGames.Framework.Runtime.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    public Pet pet;

    [SerializeField]
    public SOBalance balance;

    void Start()
    {
    }

    void Update()
    {
    }

    public void DieByFood()
    {
        SceneManager.LoadScene(0);
    }
}
