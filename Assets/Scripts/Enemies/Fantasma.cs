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
    private Collider2D collider;

    protected override void Awake()
    {
        base.Awake();
        targetRigidBody = target.GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("Awake", true);
    }

    // Update is called once per frame
    void Update()
    {
        awake = manager.awakePhantomes;

        if (awake) {
            collider.enabled = true; ;
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
            collider.enabled = false;
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
