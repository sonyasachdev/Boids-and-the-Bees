using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BumblebeeCohesion : BumbleBeeBehavior
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
                average += diff;
            }
        }
        average /= closeBees.Length;
        //possibly change to slerp
        transform.position += Vector3.Lerp(Vector3.zero, average, average.magnitude / radius);
    }

    // Update is called once per frame
    void Update()
    {
        //if something enters the sphere radius, then call method
        if(Physics.OverlapSphere(transform.position, radius, 1<<3).Length > 0)
        {
            BeesCloseBy(Physics.OverlapSphere(transform.position, radius, 1 << 3));
        }
    }
}
