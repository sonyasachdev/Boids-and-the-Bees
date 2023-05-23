using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    //Swarm 
    protected GameObject[] allBees;
    public bool activeSwarm;
    private bool hasHoney;
    private Bumblebee bee;

    //swarm movement
    private float swarmVelocity;
    private float swarmAcceleration;
    private Vector3 zeroVelocity;

    
    public float radius;

    //Destinations
    protected Vector3 hive;
    protected Vector3 swarm;
    protected Vector3 flower;

    private Vector3 swarmHomePosition;
    private Vector3 destination;

    //my methods
    private void MakeSwarm()
    {
        activeSwarm = true;

        //set velocity and acceleration
        //when approaching object, slow acceleration
        swarmVelocity = 2.0f;
        swarmAcceleration = 1.0f;

        //add all bees to swarm
        foreach(GameObject bumbleBee in allBees)
        {
            bumbleBee.transform.SetParent(gameObject.transform);
        }
        destination = flower;
    }

    IEnumerator DisbandSwarm()
    {
        //loop through all children in swarm and set parent to hive
        foreach (GameObject bumbleBee in allBees)
        {
            bumbleBee.transform.SetParent(GameObject.FindGameObjectWithTag("hive").transform);
        }

        //set default position to the idle area
        destination = swarmHomePosition;

        Debug.Log("wait start");
        //add wait for 2 seconds
        yield return new WaitForSeconds(3);
        Debug.Log("wait is over");

        //disable swarm
        activeSwarm = false;
    }

    private void PathFinder(Vector3 goal)
    {
        //when approaching destination, slowdown until stop

        swarmVelocity += swarmAcceleration * Time.deltaTime;

        //transform.position = Vector3.MoveTowards(transform.position, goal, (swarmVelocity * Time.deltaTime));

        transform.position = Vector3.SmoothDamp(transform.position, goal, ref zeroVelocity, 2.0f);
    }

    //Unity methods
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.collider.tag)
        {
            case "flower":
                hasHoney = true;
                destination = hive;
                break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "hive":
                //if bees have honey, disband swarm
                //after mvp, add to check if all bees have honey, only then will the swarm move to the hive
                if(hasHoney && activeSwarm)
                {
                    StartCoroutine(DisbandSwarm());
                    hasHoney = false;
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set all of the destinations
        hive = GameObject.FindWithTag("hive").transform.position;
        flower = GameObject.FindWithTag("flower").transform.position;
        swarm = GameObject.FindWithTag("swarm").transform.position;
        swarmHomePosition = new Vector3(-1.0f, 2.0f, 2.0f);

        allBees = GameObject.FindGameObjectsWithTag("bumblebee");
        destination = swarmHomePosition;

        //swarm movement
        swarmVelocity = 3.0f;
        swarmAcceleration = 2.0f;
        zeroVelocity = Vector3.zero;
        activeSwarm = false;
        hasHoney = false;

    }

    // Update is called once per frame
    void Update()
    {
        //only move if swarm is active
        if (activeSwarm)
        {
            PathFinder(destination);
        }

        if (!activeSwarm)
        {
            MakeSwarm();
        }
    }
}
