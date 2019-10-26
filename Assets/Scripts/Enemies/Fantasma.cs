using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : Vehicle
{
    [Min(0)] public float stoppingDistance;

    private bool facingLeft = false;
    public bool awake = false;
    
    public GameManager manager;

    Animator myAnimator;

    private Rigidbody2D targetRigidBody;

    protected override void Awake()
    {
        base.Awake();
        targetRigidBody = target.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("Awake", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (awake) {
            rigidbody.AddForce(Seek(target.transform.position), ForceMode2D.Force);

            if (facingLeft && transform.position.x < targetRigidBody.transform.position.x)
            {
                Flip();
            }
            else
            {
                if (!facingLeft && transform.position.x > targetRigidBody.transform.position.x)
                {
                    Flip();
                }
            }
        }
        else
        {
            rigidbody.AddForce(Seek(transform.position), ForceMode2D.Force);
        }

        myAnimator.SetBool("Awake", awake);

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            manager.ResetLevel();
        }
    }

    protected override Vector2 Seek(Vector3 targetPosition)
    {
        var desiredVelocity2D = DesiredVelocity(targetPosition);

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;

        steerForce = Vector2.ClampMagnitude(steerForce, maxForce);

        return steerForce;
    }

    private void Flip() {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
