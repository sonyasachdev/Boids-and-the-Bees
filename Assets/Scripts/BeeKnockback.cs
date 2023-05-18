using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeKnockback : BumbleBeeBehavior
{
    private float knockbackForce; //can just instantiate this

    // Start is called before the first frame update
    void Start()
    {
        knockbackForce = 1.0f;
        radius = 0.5f;
    }

    protected override void BeesCloseBy(Collider[] closeBees)
    {
        base.BeesCloseBy(closeBees);

        foreach (Collider bee in closeBees)
        {
            Vector3 diff = bee.transform.position - transform.position;
            if (diff.magnitude < radius)
            {
                average += diff;
            }
        }
        average /= closeBees.Length;

        //possibly change to slerp
        //see how knockback force plays into this system
        //we want to have the bees trying to run into eachother
        velocity -= Vector3.Lerp(Vector3.zero, average, average.magnitude / radius) * knockbackForce;
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
