﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public Transform target;
    float speed = .1f;
    Vector3[] path;
    int targetIndex;
<<<<<<< HEAD
    bool isAlive;

    void Update() //changed from Start for convienience
    {
        PathRequester.RequestPath(transform.position, target.position, OnPathFound);
    }
    
=======

    void Start()
    {
        PathRequester.RequestPath(transform.position, target.position, OnPathFound);
    }


>>>>>>> parent of fecc7dc... PROTOTYPE DEVELOPMENT START
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];

            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
