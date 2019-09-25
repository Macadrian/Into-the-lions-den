using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FleeFromTarget : MonoBehaviour
{
    [Min(0)] public float maxSpeed = 5;
    [Min(0)] public float maxForce = 5;
    public float distanciaMin = 3.5f;

    public Transform target;

    Rigidbody2D rigidbody;

    Vector3 velocity;
    Vector3 acceleration;
    public LayerMask mask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distTarget = Vector3.Distance(target.position, this.transform.position);
        if (distTarget <= distanciaMin)
        {
            rigidbody.AddForce(Flee(this.target.position), ForceMode2D.Force);
        }            
        else
            rigidbody.velocity = Vector2.zero;
    }

    private Vector2 Flee(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector2 desiredVelocity2D = new Vector2(-desiredVelocity.x, -desiredVelocity.y);

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;
        Vector2.ClampMagnitude(steerForce, maxForce);

        //Avoid Walls
        var hit = Physics2D.Raycast(this.transform.position, steerForce, 1f, mask);
        if (hit)
        {
            //help
        }

        Debug.DrawRay(this.transform.position, steerForce, Color.blue);
        

        return steerForce;
    }

}
