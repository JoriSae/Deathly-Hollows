using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public Transform target;
    float speed = 2;
    Vector2[] path;
    int targetIndex;

    float timer = 0.5f;
    public SphereCollider circle;
    CapsuleCollider DONTTOUCHMEH;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        circle = GetComponent<SphereCollider>();
        DONTTOUCHMEH = GetComponent<CapsuleCollider>();
    }

    // States

        //Seeking

        //Chasing

        //Attacking
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        //Search for the player
        if (timer <= 0) //&& circle.bounds.Contains(target.position))
        {
            //if (DONTTOUCHMEH.bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position) == false)
            //{
                //Chase the player
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                timer = 1f;
            //}
            //if (DONTTOUCHMEH.bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position))
            //
                //Attacking
                //StopAllCoroutines();
            //}
        }
    }


    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {

        if (pathSuccessful)
        {
            StopCoroutine("FollowPath");
            path = newPath;
            targetIndex = 0;
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
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