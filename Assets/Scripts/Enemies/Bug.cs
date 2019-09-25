using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Vehicle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(Flee(target.transform.position), ForceMode2D.Force);
    }

    protected override Vector2 Flee(Vector3 targetPosition)
    {
        Vector2 steerForce = base.Flee(targetPosition);
        Debug.DrawRay(transform.position, steerForce.normalized, Color.blue);
        return steerForce;
    }
}
