using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeContainer : BumbleBeeBehavior
{
    private float swarmMagnetism;
    // Start is called before the first frame update
    void Start()
    {
        swarmMagnetism = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > radius)
        {
            velocity += transform.position.normalized * (radius - transform.position.magnitude) * swarmMagnetism * Time.deltaTime;
        }
    }
}
