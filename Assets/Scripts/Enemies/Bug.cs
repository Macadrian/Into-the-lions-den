using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Vehicle
{

    public LayerMask mask;
    public float velovity = 5;
    public float distanciaMin = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float distTarget = Vector3.Distance(GameManager.Instance.playerTransform.position, transform.position);
        Vector2 steerForce = Flee(GameManager.Instance.playerTransform.position);

        if (distTarget <= distanciaMin)
        {
            var hit = Physics2D.Raycast(transform.position, steerForce, 1.5f, mask);
            if (hit)
            {
                if (hit.normal.x == 1.0) steerForce.x *= -hit.normal.x;
                else if (hit.normal.x == -1.0) steerForce.x *= hit.normal.x;

                if (hit.normal.y == 1.0) steerForce.y *= -hit.normal.y;
                else if (hit.normal.y == -1.0) steerForce.y *= hit.normal.y;
            }

            Debug.DrawRay(transform.position, steerForce.normalized, Color.blue);
            rigidbody.AddForce(steerForce, ForceMode2D.Force);
        }

    }

    protected override Vector2 Flee(Vector3 targetPosition)
    {
        Vector2 steerForce = base.Flee(targetPosition);

        return steerForce;
    }

    /*protected Vector2 Seek()
    {
        //var desiredVelocity2D = ;

        Vector2 steerForce = desiredVelocity2D - rigidbody.velocity;

        steerForce = Vector2.ClampMagnitude(steerForce, maxForce);

        return steerForce;
    }*/
}
