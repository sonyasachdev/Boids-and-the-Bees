using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBee : MonoBehaviour
{
    //Bee
    private GameObject[] allBees;
    public GameObject bumbleBee; //passed in through unity
    public int beeCount; //passed in through unity
    private float xPos;
    private float yPos;
    private float zPos;

    //Spawn
    public float spawnLength; //how big the spawn area is

    //My Methods
    private void SpawnBees(GameObject bee, int beeCount)
    {
        int count = 0;
        while(count < beeCount)
        {
            //make random position
            xPos = Random.Range((transform.position.x - spawnLength), (transform.position.x + spawnLength));
            yPos = Random.Range((transform.position.y - spawnLength), (transform.position.y + spawnLength));
            zPos = Random.Range((transform.position.z - spawnLength), (transform.position.z + spawnLength));

            Instantiate(bee, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90,90,0), transform);
            count++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnBees(bumbleBee, beeCount);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
