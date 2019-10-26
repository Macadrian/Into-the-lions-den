using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Vehicle
{

    public LayerMask mask;
    public float distanciaMin = 3.5f;

    public GameManager manager;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = manager.playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        float distTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distTarget <= distanciaMin)
        {
            Vector2 steerForce = Flee(target.transform.position);
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
        else
            rigidbody.velocity = Vector2.zero;
    }

    protected override Vector2 Flee(Vector3 targetPosition)
    {
        Vector2 steerForce = base.Flee(targetPosition);

        return steerForce;
    }
}
