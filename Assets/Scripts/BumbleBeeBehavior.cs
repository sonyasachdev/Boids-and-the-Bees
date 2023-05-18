using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbleBeeBehavior : Bumblebee
{
    //Shared fields
    protected float radius;
    protected Vector3 average;

    //Shared Methods
    protected virtual void BeesCloseBy(Collider[] closeBees)
    {
        average = Vector3.zero;
    }

    //sets radius for everything inheriting from this script
    private void Awake()
    {
        radius = 5.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
