using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private GameManager gameManager;

    public float MOVEMENT_BASE_SPEED;

    Rigidbody2D rigidbody2D;
    private Vector2 movementDirection;

    //evitar que con diferentes mados o input devices se cambie como se mueve el jugador
    private float movementSpeed;

    //Vector3 movement;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.playerTransform = transform;

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementDirection.x = Input.GetAxis("Horizontal");
        movementDirection.y = Input.GetAxis("Vertical");
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1.0f);
        movementDirection.Normalize();

        rigidbody2D.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;

        gameManager.playerTransform = transform;

    }
}
