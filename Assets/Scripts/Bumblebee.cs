using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumblebee : MonoBehaviour
{
    //layermask
    [SerializeField] private LayerMask layerMask;

    //movement
    protected float speed;
    protected float acceleration;
    protected Rigidbody rb;
    public Vector3 velocity;
    public float maxVelocity;

    //boid system variables
    protected int clumsiness; //how much they bump into others
    protected int cohesiveness; //how much they want to stay together

    //the boid system will always want to go to the center of the swarm
    //to make sure that the bees do not just stay in the same spot, make sure that they bounce off eachother

    //my methods
    //bee will always want to go to the center of the swarm.
    protected void Movement()
    {
        //make movement circular and bumbly
        //acceleration code and pathfinding code to center of swarm
        //speed += acceleration * Time.deltaTime;
        
        //my move code
        //basic move code
        //i want to use slerp because its more circular
//        transform.position = Vector3.Slerp(transform.position, swarmCenter.transform.position, (speed * Time.deltaTime));

        //use the boid system variables here to influence movement

        //makes sure that the velocity never exceeds the maxVelocity
        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity; //check if use rb.velocity instead of this velocity
        }
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    //Unity Methods
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "bumblebee": //bumblebee
                //Debug.Log("bumble bump");
                //knockback on bump

                //rb.AddForce(rb.velocity * -1, ForceMode.Impulse);
                //movement on both bees stop as they 'bump' into eachother and then gradually pick up speed again
                break;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //bounce back into swarm area
        /*Debug.Log("bees left swarm");
        //transform.forward *-1;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*speed = 3.0f;
        acceleration = 2.0f; //make slower for accel, maybe 1.2 next when you see it works?
        clumsiness = Random.Range(5, 10);
        cohesiveness = Random.Range(6, 8);*/

        //hive = GameObject.FindGameObjectWithTag("hive");

        //gameObject.transform.SetParent(hive.transform);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move to center of parent always
        //Movement(gameObject.transform.parent.gameObject);
        Movement();
    }
}
