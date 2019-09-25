using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : Vehicle
{
    [Min(0)] public float stoppingDistance;

    private Rigidbody2D targetRigidBody;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        targetRigidBody = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(Seek(target.transform.position), ForceMode2D.Force);

    }

    protected override Vector2 Seek(Vector3 targetPosition)
    {
        var desiredVelocity2D = DesiredVelocity(targetPosition);

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;

        steerForce = Vector2.ClampMagnitude(steerForce, maxForce);

        return steerForce;
    }
}
