﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Vehicle : MonoBehaviour
{
    [Min(0)] public float maxSpeed = 5;
    [Min(0)] public float maxForce = 5;

    public GameObject target;

    protected Rigidbody2D rigidbody;

    Vector3 velocity;
    Vector3 acceleration;


    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual Vector2 Seek(Vector3 targetPosition)
    {
        var desiredVelocity2D = DesiredVelocity(targetPosition);

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;

        steerForce = Vector2.ClampMagnitude(steerForce, maxForce);

        return steerForce;
    }

    protected virtual Vector2 Flee(Vector3 targetPosition)
    {
        var desiredVelocity2D = -DesiredVelocity(targetPosition);

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;

        Vector2.ClampMagnitude(steerForce, maxForce);

        return steerForce;
    }

    protected virtual Vector2 DesiredVelocity(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        return new Vector2(desiredVelocity.x, desiredVelocity.y);
    }
}
