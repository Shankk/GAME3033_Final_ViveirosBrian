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

    [Header("Floater (Does Not Work on Obstacles)")]
    public bool FloatObject = false;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        SpawnObjects();
        posOffset = transform.position;
    }

    private void FixedUpdate()
    {
        if(FloatObject && !spawnObstacles)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
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
            else
            {
                Destroy(transform.gameObject);
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
            else
            {
                Destroy(transform.gameObject);
            }
        }

        if(spawnScrap)
        {
           var cloneObj = Instantiate(Scrap, transform.position, transform.rotation);
           cloneObj.transform.SetParent(gameObject.transform);
        }
        
    }
}
