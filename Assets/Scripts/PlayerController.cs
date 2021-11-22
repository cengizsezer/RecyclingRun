using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("VECTORS")]
    public Vector3 moveVector;
    public Vector3 lastMove;
    [Header("STATS")]
    [Space]
    [Range(0f,20f)]public float speed = 8f;
    [Range(0f, 20f)] public float jumpForce = 8f;
    [Range(0f, 20f)] public float gravity = 25f;
    [Range(0f, 20f)] public float verticalVelocity;
    [Range(0f, 20f)] public float dashSpeed;
    [Range(0f, 20f)] public float dashTime;
    [Space]
    [Header("COMPONENT")]
    public CharacterController controller;

    GameController _gameController;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _gameController = GameController.request();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { StartCoroutine(Dasher()); }
        else if (Input.GetKeyDown(KeyCode.O)) { _gameController.LostGame(); }
        else if (Input.GetKeyDown(KeyCode.P)) { _gameController.WinGame(); }



        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.z = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            verticalVelocity = -1;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
        }

        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= speed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
                verticalVelocity = jumpForce;
                moveVector = hit.normal * speed;
            }
        }
    }


    IEnumerator Dasher()
    {
        
        float startTime = Time.deltaTime;

        while (Time.time < startTime + dashTime)
        {
            controller.Move(moveVector * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    
}
