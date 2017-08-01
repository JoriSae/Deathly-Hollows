using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{

    public GameObject Leader;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    public int moveSpeed;
    public int rotationSpeed;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;

    // Use this for initialization
    void Start()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);

        if (force.magnitude > Leader.GetComponent<AllUnits>().maxForce)
        {
            force = force.normalized;
            force *= Leader.GetComponent<AllUnits>().maxForce;
        }
        this.GetComponent<Rigidbody2D>().AddForce(force);

        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > Leader.GetComponent<AllUnits>().maxVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
            this.GetComponent<Rigidbody2D>().velocity *= Leader.GetComponent<AllUnits>().maxVelocity;
        }
        Debug.DrawRay(this.transform.position, force, Color.white);
    }

    Vector2 align()
    {
        float neighbourdist = Leader.GetComponent<AllUnits>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject other in Leader.GetComponent<AllUnits>().units)
        {
            if (other == this.gameObject)
                continue;

            float d = Vector2.Distance(location, other.GetComponent<FlockUnit>().location);

            if (d < neighbourdist)
            {
                sum += other.GetComponent<FlockUnit>().velocity;
                count++;
            }

            if (count > 0)
            {
                sum /= count;
                Vector2 steer = sum - velocity;
                return steer;
            }
        }
        return Vector2.zero;
    }

    Vector2 cohesion()
    {
        float neighbourdist = Leader.GetComponent<AllUnits>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject other in Leader.GetComponent<AllUnits>().units)
        {
            if (other == this.gameObject)
                continue;

            float d = Vector2.Distance(location, other.GetComponent<FlockUnit>().location);

            if (d < neighbourdist)
            {
                sum += other.GetComponent<FlockUnit>().location;
                count++;
            }
        }

        if (count > 0)
        {
            sum /= count;
            return seek(sum);
        }

        return Vector2.zero;
    }

    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        if (Leader.GetComponent<AllUnits>().flocking && Random.Range(0, 50) <= 1)
        {

            Vector2 ali = align();
            Vector2 coh = cohesion();
            Vector2 gl;
            if (Leader.GetComponent<AllUnits>().seekGoal)
            {
                gl = seek(goalPos);
                currentForce = gl + ali + coh;
            }
            else
                currentForce = ali + coh;

            currentForce = currentForce.normalized;
        }

        if (Leader.GetComponent<AllUnits>().willful && Random.Range(0, 50) <= 1)
        {
            if (Random.Range(0, 50) < 1)
                currentForce = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        }

        applyForce(currentForce);
    }

    void seekLeader()
    {
        if (Leader.GetComponent<AllUnits>().seekGoal)
        {
            Vector3 dir = seek(goalPos);
                // Only needed if objects don't share 'z' value.
                dir.z = 0.0f;
                if (dir != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.FromToRotation(Vector3.right, dir),
                        rotationSpeed * Time.deltaTime);

                //Move Towards Target
                transform.position += (Leader.transform.position - transform.position).normalized
                    * moveSpeed * Time.deltaTime;
            //this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }


    // Update is called once per frame
    void Update()
    {
        seekLeader();
        flock();
        goalPos = Leader.transform.position;
    }
}
