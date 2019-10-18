﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float speed = 2.5f;
    public float turnSpeed = 2.5f;
    public float turnDst = 5;

    Quaternion rotation;

    /*Vector3[] path;
    int targetIndex; --> */
    Path path;

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
            path = new Path(waypoints, transform.position, turnDst);
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
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

                rotation = transform.rotation;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                transform.rotation = Quaternion.identity;
            }
            yield return null;
        }
    }

    /*IEnumerator FollowPathOld()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    pathEnded = true;
                    currentPath = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }*/

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}