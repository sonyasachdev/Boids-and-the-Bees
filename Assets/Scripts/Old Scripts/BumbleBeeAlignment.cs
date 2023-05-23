using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BumbleBeeAlignment : BumbleBeeBehavior
{
    // Start is called before the first frame update
    void Start()
    {
    }

    protected override void BeesCloseBy(Collider[] closeBees)
    {
        base.BeesCloseBy(closeBees);
        foreach (Collider bee in closeBees)
        {
            Vector3 diff = bee.transform.position - transform.position;
            if (diff.magnitude < radius)
            {
                average += velocity;
            }
        }
        average /= closeBees.Length;
        //possibly change to slerp
        velocity += Vector3.Lerp(velocity, average, Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.OverlapSphere(transform.position, radius, 1 << 3).Length > 0)
        {
            BeesCloseBy(Physics.OverlapSphere(transform.position, radius, 1 << 3));
        }
    }
}
