using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rigidbody2D;

    Vector3 movement;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement.z = 0;

        if (movement.x > 0) { } 
        else if (movement.x < 0) { }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement.normalized, speed * Time.deltaTime);


    }
}
