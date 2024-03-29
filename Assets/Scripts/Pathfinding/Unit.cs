﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .2f;

    public Transform target;
    public float speed = 2.5f;
    public float turnSpeed = 2.5f;
    public float turnDst = 5;

    private bool facingLeft = false;
    public bool followingPath;

    int targetIndex;
    Vector3[] path;

    public GameObject vision;

    public bool currentPath;

    public bool jugadorAlcanzado = false;

    void Start()
    {
        currentPath = false;
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = waypoints;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .2f)
            yield return new WaitForSeconds(.2f);
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        followingPath = true;
        Vector3 currentWayPoint = path[0];

        while (followingPath)
        {

            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    currentPath = true;
                    followingPath = false;
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);

            if (facingLeft && transform.position.x < currentWayPoint.x)
            {
                Flip();
            }
            else
            {
                if (!facingLeft && transform.position.x > currentWayPoint.x)
                {
                    Flip();
                }
            }

            yield return null;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jugadorAlcanzado = true;
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        Vector3 rotation = new Vector3(0, 0, 180);
        vision.transform.Rotate(rotation, Space.Self);
    }

    /*IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    currentPath = true;
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                transform.rotation = rotation;
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                //visionSensor.rotation = targetRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

                rotation = transform.rotation;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                transform.rotation = Quaternion.identity;
            }
            yield return null;
        }
    }*/

    /*const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float speed = 2.5f;
    public float turnSpeed = 2.5f;
    public float turnDst = 5;

    int targetIndex;
    Vector3[] path;

    Quaternion rotation;
    
    //Path path;

    public bool currentPath;
    
    void Start()
    {
        rotation = Quaternion.identity;
        currentPath = false;
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            //path = new Path(waypoints, transform.position, turnDst);
            path = waypoints;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .2f)
            yield return new WaitForSeconds(.2f);
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        Vector3 currentWayPoint = path[0];      

        while (followingPath)
        {

            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    currentPath = true;
                    followingPath = false;
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    /*IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    currentPath = true;
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                transform.rotation = rotation;
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                //visionSensor.rotation = targetRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

                rotation = transform.rotation;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                transform.rotation = Quaternion.identity;
            }
            yield return null;
        }
    }*/

}