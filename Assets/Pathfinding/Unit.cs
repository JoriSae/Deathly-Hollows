using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public Transform target;
    float speed = 2;
    Vector2[] path;
    int targetIndex;

    private void Start()
    {

    }
    private void Update()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }


    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector2 currentWayPoint = path[0];

        while (true)
        {
            if ((Vector2)transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }

                currentWayPoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);
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
                Gizmos.DrawCube(path[i], new Vector2(0.2f,0.2f));

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}