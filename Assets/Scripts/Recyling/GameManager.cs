using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> ActiveObjectList;
    GameController gameController;
    PlayerController playerController;


    private void Start()
    {
        gameController = GameController.request();
        playerController = PlayerController.request();
        ActiveObjectList = new List<GameObject>();
    }

    private void Update()
    {
        if(ActiveObjectList.Count>=20 && playerController.NumberOfFallingObjects<5)
        {
            gameController.WinGame();
        }
    }



}
