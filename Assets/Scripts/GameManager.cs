using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> ActiveObjectList;
    public int NumberOfFallingObjects;
    GameController gameController;


    private void Start()
    {
        gameController = GameController.request();
        ActiveObjectList = new List<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Obstacles")
        {
            NumberOfFallingObjects++;

            if(NumberOfFallingObjects>=5)
            {
                gameController.LostGame();
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
}
