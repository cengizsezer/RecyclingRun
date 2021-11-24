using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer meshRenderer;
    GameController gameController;

    [SerializeField] float speed = 5f, materialSpeed = 1f;

    float yOffset = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer= GetComponent<MeshRenderer>();
        gameController = GameController.request();
    }

    private void FixedUpdate()
    {
        if (gameController.IsGameStarted == false) { return; }
        Vector3 pos = rb.position;
        rb.position -= transform.forward * Time.fixedDeltaTime * speed;
        rb.MovePosition(pos);

        yOffset += Time.fixedDeltaTime * materialSpeed;
        meshRenderer.material.mainTextureOffset = new Vector3(0, yOffset);

    }
}
