using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : Vehicle
{
    [Min(0)] public float stoppingDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float remainingDistance = Vector3.Distance(target.position, transform.position);

        if (remainingDistance > stoppingDistance)
        {
            rigidbody.AddForce(Seek(target.position), ForceMode2D.Force);
        }
    }

    protected override Vector2 Seek(Vector3 targetPosition)
    {
        Vector2 steerForce = base.Seek(targetPosition);

        return steerForce;
    }
}
