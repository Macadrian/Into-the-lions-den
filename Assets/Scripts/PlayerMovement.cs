using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject action_btn;
    private GameManager gameManager;

    Rigidbody2D rigidbody2D;
    private Animator myAnimator;

    private Vector2 movementDirection;
    private bool facingRight = true;

    //evitar que con diferentes mados o input devices se cambie como se mueve el jugador
    private float movementSpeed;

    //Vector3 movement;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
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

        rigidbody2D.velocity = movementDirection * speed;

        gameManager.playerTransform = transform;

        setAnimations();

    }

    private void setAnimations() {
        if(facingRight && movementDirection.x < 0)
        {
            flip();
        }
        else
        {
            if (!facingRight && movementDirection.x > 0)
            {
                flip();
            }
        }

        if (movementDirection.x == 0 && movementDirection.y == 0) {
            myAnimator.SetBool("Walking", false);
        }
        else
        {
            myAnimator.SetBool("Walking", true);
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
