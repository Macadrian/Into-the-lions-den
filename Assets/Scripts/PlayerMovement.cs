﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [HideInInspector] public float frictionInMudPercentage = 0f;
    public GameObject action_btn;
    public List <GameObject> bloodStains;
    private GameManager gameManager;

    Rigidbody2D rigidbody2D;
    private Animator myAnimator;

    public bool canMove = true;

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
        if(canMove)
        {
            movementDirection.x = Input.GetAxis("Horizontal");
            movementDirection.y = Input.GetAxis("Vertical");
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1.0f);
            movementDirection.Normalize();

            rigidbody2D.velocity = movementDirection * (speed - speed * frictionInMudPercentage);

            gameManager.playerTransform = transform;

            setAnimations();
        }
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

            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null) {
                audioManager.Play("PlayerFootsteps");
            }
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

    public void Reset(Vector3 position)
    {
        if (bloodStains != null)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            int stainIndex = Random.Range(0, bloodStains.Count);
            Instantiate(bloodStains[stainIndex], transform.position, rotation);
        }
        transform.position = position;
    }
}
