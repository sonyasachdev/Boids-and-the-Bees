using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    private GameObject[] allBees;

    private Rigidbody rb;
    private Vector3 diff;
    public float minVelocity;
    public float maxVelocity;
    private float xVel;
    private float yVel;
    private float zVel;

    public Vector3 knockBack;

    //movewment
    private Vector3 centerSwarm;

    //My Methods
    private void SetUpBeeVelocity()
    {
        //make random velocity
        xVel = Random.Range(minVelocity, maxVelocity);
        yVel = Random.Range(minVelocity, maxVelocity);
        zVel = Random.Range(minVelocity, maxVelocity);

        //check if add force
        rb.velocity = new Vector3(xVel, yVel, zVel);
    }
    private Vector3 MeanVector()
    {
        //grab all bees
        allBees = GameObject.FindGameObjectsWithTag("bumblebee");
        Vector3 meanVector = Vector3.zero;

        foreach (GameObject bee in allBees)
        {
            diff = bee.transform.position - transform.position;
            //average formula for position
            meanVector += diff;
        }

        //returns center of all bees
        return (meanVector / allBees.Length);
    }

    private void MoveBeeToCenterSwarm()
    {
        //simplest terms:
        //have a force that is constantly spanking the bee into the mdidl eof the swarm
        //the force is an equation which will be something having to do with the difference of where the object is versis where it wants to be
        //the bee needs to be able to be knocked out of the middle
        //whatever bee is in the middle will want to move somewhere else and will act as the leader

        //this is cohesion - slerp for bumbly
        rb.velocity += Vector3.Slerp(Vector3.zero, centerSwarm, centerSwarm.magnitude/5);

        //rotation always look at the middle of the swarm
        transform.rotation = Quaternion.LookRotation(centerSwarm);

        //this is bumpers - lerp
        //rb.velocity = Vector3.Lerp(Vector3.zero, centerSwarm, centerSwarm.magnitude / 5) * 2;

        //this is staying within bounds - lerp

        //rb.AddForce(Vector3.Lerp(Vector3.zero, centerSwarm, centerSwarm.magnitude / 5), ForceMode.Force);

        //Vector3.Lerp(transform.position, centerSwarm, centerSwarm.magnitude/5);
        //rb.AddForce(transform.position, ForceMode.Force);

        //if hit bound, bound by applying inverse vector

        //min velocity
        //caps out max velocity
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
        else if(rb.velocity.magnitude < minVelocity)
        {
            rb.velocity = rb.velocity.normalized * minVelocity;
        }

        //if(rb.transform.position)
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "bumblebee":
                //rb.AddRelativeForce(rb.velocity/-2, ForceMode.Acceleration);
                rb.velocity *= -1;
                break;
            /*case "swarm":
                Debug.Log("in bounds");
                break;*/
        }
    }
/*
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "swarm":
                Debug.Log("out bounds");
                rb.velocity *= -1;
                rb.AddForceAtPosition(rb.velocity * -1, other.ClosestPointOnBounds(rb.transform.position), ForceMode.Force);
                break;
        }
    }
*/
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        diff = Vector3.zero;
        SetUpBeeVelocity();
        allBees = GameObject.FindGameObjectsWithTag("bumblebee");
        centerSwarm = MeanVector();
    }

    void Update()
    {
        centerSwarm = MeanVector();
        MoveBeeToCenterSwarm();
    }
}
