using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rigidbody2D;

    Vector2 movement;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x > 0) { } 
        else if (movement.x < 0) { } 
    }

    private void FixedUpdate()
    {
        rigidbody2D.position +=  (movement.normalized * speed * Time.deltaTime);
    }
}
