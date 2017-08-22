using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{

    public GameObject Leader;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    public float moveSpeed;
    public int rotationSpeed;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;
    bool FoundPlayer = false;

    float timer = 5;

    public GameObject[] buildings;
    public List<GameObject> WoodenBuilding = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        buildings = GameObject.FindGameObjectsWithTag("Building");


        foreach (GameObject i in buildings)
        {
            if (i.GetComponent<SpriteRenderer>().sprite.name == "Wall")
            {
                WoodenBuilding.Add(i);
            }

            if (i.GetComponent<SpriteRenderer>().sprite.name == "Tower1")
            {
                WoodenBuilding.Add(i);
            }
        }
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
            if (other != null)
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
            if (other != null)
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
                //currentForce = (gl + ali + coh) * moveSpeed * Time.deltaTime;
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
        if (Leader != null)
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

                transform.position += (Leader.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
                //this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (!FoundPlayer)
        {
            if ((this.transform.position.x - FindObjectOfType<Player>().transform.position.x) <= 3 && (this.transform.position.y - FindObjectOfType<Player>().transform.position.y) <= 3)
            {
                this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units.Remove(this.gameObject);
                FoundPlayer = true;
                Leader = GameObject.FindGameObjectWithTag("Player");

                Leader.GetComponent<AllUnits>().units.Add(this.gameObject);
            }
        }


        timer -= Time.deltaTime;

        if (FoundPlayer)
        {
            if (timer <= 0)
            {

                buildings = GameObject.FindGameObjectsWithTag("Building");
                WoodenBuilding.Clear();
                foreach (GameObject i in buildings)
                {
                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Wall")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Door1")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Walld1")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Walld2")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Walld3")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Tower1")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Tower2")
                    {
                        WoodenBuilding.Add(i);
                    }

                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Tower3")
                    {
                        WoodenBuilding.Add(i);
                    }
                    if (i.GetComponent<SpriteRenderer>().sprite.name == "Tower4")
                    {
                        WoodenBuilding.Add(i);
                    }
                }


                for (int go = 0; go < WoodenBuilding.Count; go++)
                {
                    if ((GameObject.FindGameObjectWithTag("Player").transform.position.x - this.transform.position.x) >= (WoodenBuilding[go].transform.position.x - this.transform.position.x)
                       && (GameObject.FindGameObjectWithTag("Player").transform.position.y - this.transform.position.y) >= (WoodenBuilding[go].transform.position.y - this.transform.position.y))
                    {
                        this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units.Remove(this.gameObject);
                        Leader = WoodenBuilding[go].gameObject;
                        Leader.GetComponent<AllUnits>().units.Add(this.gameObject);

                        timer = 1f;
                    }

                    if (Leader == null)
                        Leader = GameObject.FindGameObjectWithTag("Player");

                    for (int go1 = 0; go1 < WoodenBuilding.Count; go1++)
                    {
                        if ((GameObject.FindGameObjectWithTag("Player").transform.position.x - this.transform.position.x) >= (WoodenBuilding[go1].transform.position.x - this.transform.position.x)
                           && (GameObject.FindGameObjectWithTag("Player").transform.position.y - this.transform.position.y) >= (WoodenBuilding[go1].transform.position.y - this.transform.position.y))
                        {
                            this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units.Remove(this.gameObject);
                            Leader = WoodenBuilding[go].gameObject;
                            Leader.GetComponent<AllUnits>().units.Add(this.gameObject);
                            timer = 1f;
                        }

                        else if ((GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x) < (WoodenBuilding[go1].transform.position.x - transform.position.x)
                                  && (GameObject.FindGameObjectWithTag("Player").transform.position.y - transform.position.y) < (WoodenBuilding[go1].transform.position.y - transform.position.y))
                        {
                            this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units.Remove(this.gameObject);
                            Leader = GameObject.FindGameObjectWithTag("Player");
                            Leader.GetComponent<AllUnits>().units.Add(this.gameObject);
                            timer = 1f;
                        }
                    }
                }
            }

            if (this != null)
            {
                seekLeader();
                flock();
                goalPos = Leader.transform.position;
            }
        }
    }
}

