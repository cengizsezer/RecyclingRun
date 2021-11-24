using DG.Tweening;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Camera Camera;
    public GameObject CapturedObject;
    public ObstacleObject ObstacleObject;

    public Transform Glass_Trashbin;
    public Transform Paper_Trashbin;
    public Transform Plastic_Trashbin;
    public Transform Organic_Trashbin;

    GameController gameController;

    public Vector3 PreviousPosition;
    public int NumberOfFallingObjects;

    private void Start()
    {
        Camera = Camera.main;
        gameController = GameController.request();
        LeanTouch.OnFingerUpdate += FingerUpdate;

    }

    private void FingerUpdate(LeanFinger leanFinger)
    {
        if (!gameController.IsGameStarted) return;

        Vector3 currentPosition;

        if (leanFinger.Down)
        {
            Ray ray = leanFinger.GetRay(Camera);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.CompareTag(Tag.Obstacles.ToString()))
                    {
                        Debug.LogError(hit.transform.gameObject.name);

                        ObstacleObject = hit.transform.gameObject.GetComponent<ObstacleObject>();
                    }
                }
            }
            PreviousPosition = leanFinger.ScreenPosition;

        }


        if (leanFinger.Up && ObstacleObject != null)
        {
            currentPosition = leanFinger.ScreenPosition;
            Vector3 targetPos = Vector3.zero;

            if (currentPosition.x > PreviousPosition.x)
            {
                if (ObstacleObject.ObstacleType == WasteType.Paper)
                {
                    targetPos = Paper_Trashbin.position;
                  
                }
                else if (ObstacleObject.ObstacleType == WasteType.Glass)
                {
                    targetPos = Glass_Trashbin.position;
                   

                }
             

                targetPos.y -= 1;

                ObstacleObject.transform.DOJump(targetPos, 5, 1, 1);
            }
            else
            {
                if (ObstacleObject.ObstacleType == WasteType.Organic)
                {
                    targetPos = Organic_Trashbin.position;
                }
                else if (ObstacleObject.ObstacleType == WasteType.Plastic)
                {
                    targetPos = Plastic_Trashbin.position;

                }
              

                targetPos.y -= 1;

                ObstacleObject.transform.DOJump(targetPos, 5, 1, 1);
            }

            ObstacleObject.GetComponent<Rigidbody>().isKinematic = true;
            ObstacleObject = null;


        }
    }

    private void OnDestroy()
    {
        LeanTouch.OnFingerUpdate -= FingerUpdate;
    }


}
