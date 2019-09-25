using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Vehicle : MonoBehaviour
{
    [Min(0)] public float maxSpeed = 5;
    [Min(0)] public float maxForce = 5;

    public Transform target;

    Rigidbody2D rigidbody;

    Vector3 velocity;
    Vector3 acceleration;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(Seek(this.target.position) ,ForceMode2D.Force);
    }

    private Vector2 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        var desiredVelocity2D = new Vector2(desiredVelocity.x, desiredVelocity.y);

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;

        Vector2.ClampMagnitude(steerForce, maxForce);

        return steerForce;
    }
}
