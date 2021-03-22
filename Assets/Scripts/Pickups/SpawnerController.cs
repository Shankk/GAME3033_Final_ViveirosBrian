using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] Obstacles;
    public GameObject[] Pickups;
    public GameObject Scrap;
    public bool spawnPickups = false;
    public bool spawnObstacles = false;
    public bool spawnScrap = false; 
    public float chanceToSpawn = 25f;

    private void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        var chance = Random.Range(0, 100);
        
        if (spawnObstacles)
        {
            if (chance < chanceToSpawn)
            {
                GameObject objectFromList = Obstacles[Random.Range(0, Obstacles.Length)];
                var cloneObj = Instantiate(objectFromList, transform.position, transform.rotation);
                cloneObj.transform.SetParent(gameObject.transform);
            }
        }

        if(spawnPickups)
        {
            if (chance < chanceToSpawn)
            {
                GameObject objectFromList = Pickups[Random.Range(0, Pickups.Length)];
                var cloneObj = Instantiate(objectFromList, transform.position, transform.rotation);
                cloneObj.transform.SetParent(gameObject.transform);
            }
        }

        if(spawnScrap)
        {
           var cloneObj = Instantiate(Scrap, transform.position, transform.rotation);
           cloneObj.transform.SetParent(gameObject.transform);
        }
        
    }
}
