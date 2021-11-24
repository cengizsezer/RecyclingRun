using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjectTrigger : MonoBehaviour
{
    PlayerController playerController;
    GameController gameController;

    private void Start()
    {
        playerController = PlayerController.request();
        gameController = GameController.request();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Obstacles.ToString()))
        {
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.4f);

            playerController.NumberOfFallingObjects++;

            if (playerController.NumberOfFallingObjects >= 5)
            {
                gameController.LostGame();
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
}
