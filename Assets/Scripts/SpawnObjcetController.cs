using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjcetController : MonoBehaviour
{
    GameController gameController;
    GameManager gameManager;

    Vector3 spawnPosition;
    public bool StopSpawning = false;

    [SerializeField] WasteType wasteType;

    public GameObject PaperObjcet;
    public GameObject GlassObjcet;
    public GameObject PlasticObjcet;
    public GameObject OrganicObjcet;

    [SerializeField] float SpawnTime = 0f;
    [SerializeField] float Delay = 0f;
    [SerializeField] int MaxObjcetSpwan = 50;

    void Start()
    {
        
        gameController = GameController.request();
        gameManager = GameManager.request();
        spawnPosition = transform.position;

        InvokeRepeating(nameof(SpawnObject), SpawnTime, Delay);
        
    }

    private void Update()
    {
        int NumberOfActiveObjects = gameManager.ActiveObjectList.Count;
        if(NumberOfActiveObjects>=MaxObjcetSpwan)
        {
            StopSpawning = true;

            if(StopSpawning==true && gameController.IsGameStarted==false)
            {
                gameController.WinGame();
            }
           
        }
    }

    public void SpawnObject()
    {
        GameObject obj = null;
        Vector3 pos = spawnPosition;

        if (wasteType == WasteType.Paper)
        {
           
            obj = Instantiate(PaperObjcet, pos, PaperObjcet.transform.rotation);
            gameManager.ActiveObjectList.Add(obj);
            if (StopSpawning)
            {
                CancelInvoke(nameof(SpawnObject));
            }

        }
        else if (wasteType == WasteType.Glass)
        {
           
            obj = Instantiate(GlassObjcet, pos, GlassObjcet.transform.rotation);
            gameManager.ActiveObjectList.Add(obj);
            if (StopSpawning)
            {
                CancelInvoke(nameof(SpawnObject));
            }

        }
        else if (wasteType == WasteType.Plastic)
        {
           
            obj = Instantiate(PlasticObjcet, pos, PlasticObjcet.transform.rotation);
            gameManager.ActiveObjectList.Add(obj);
            if (StopSpawning)
            {
                CancelInvoke(nameof(SpawnObject));
            }

        }
        else if (wasteType == WasteType.Organic)
        {

            obj = Instantiate(OrganicObjcet, pos, OrganicObjcet.transform.rotation);
            obj.transform.Rotate(new Vector3(0, 180, 0));
            gameManager.ActiveObjectList.Add(obj);
            if (StopSpawning)
            {
                CancelInvoke(nameof(SpawnObject));
            }

        }

    }

    
   
}
