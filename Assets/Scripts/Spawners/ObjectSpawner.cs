using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public SpawnPreset ObjectToSpawn;
    public bool hasChanceToSpawn = false;
    
    [Header("Group Spawn Settings")]
    public bool spawnGroup = false;
    public int MinAmountToSpawn = 1;
    public int MaxAmountToSpawn = 5;
    public float MinSpawnDistance = 5f;
    public float MaxSpawnDistance = 15f;
    
    void Start()
    {
        if (spawnGroup)
        {
            CreateGroup(ObjectToSpawn);
        }
        else
        {
            CreateSingle(ObjectToSpawn);
        }
    }

    public void CreateSingle(SpawnPreset item)
    {
        var chance = Random.Range(0, 100);
        if(item != null)
        {
            if (hasChanceToSpawn)
            {
                if (chance <= item.CurrentSpawn)
                {
                    var cloneObj = Instantiate(item.Object, transform.position, transform.rotation);
                    cloneObj.transform.SetParent(gameObject.transform);
                }
                else
                {
                    Destroy(transform.gameObject);
                }
            }
            else
            {
                var cloneObj = Instantiate(item.Object, transform.position, transform.rotation);
                cloneObj.transform.SetParent(gameObject.transform);
            }
        }
        else
        {
            Debug.Log("Cant Spawn Item! Item Is Invalid/Not Existent");
        }
    }

    public void CreateGroup(SpawnPreset item)
    {
        var chance = Random.Range(0, 100);
        if (hasChanceToSpawn)
        {
            if (chance <= item.CurrentSpawn)
            {
                for (int i = 0; i < Random.Range(MinAmountToSpawn, MaxAmountToSpawn); i++)
                {
                    var SpawnPos = new Vector3(
                    transform.position.x + (Random.Range(0, 100) > 50 ? Random.Range(-MinSpawnDistance, -MaxSpawnDistance) : Random.Range(MinSpawnDistance, MaxSpawnDistance)),
                    transform.position.y,
                    transform.position.z + (Random.Range(0, 100) > 50 ? Random.Range(-MinSpawnDistance, -MaxSpawnDistance) : Random.Range(MinSpawnDistance, MaxSpawnDistance)));

                    var cloneObj = Instantiate(item.Object, SpawnPos, transform.rotation);
                    cloneObj.transform.SetParent(gameObject.transform);
                }
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }
        else
        {
            for (int i = 0; i < Random.Range(MinAmountToSpawn, MaxAmountToSpawn); i++)
            {
                var SpawnPos = new Vector3(
                transform.position.x + (Random.Range(0, 100) > 50 ? Random.Range(-MinSpawnDistance, -MaxSpawnDistance) : Random.Range(MinSpawnDistance, MaxSpawnDistance)),
                transform.position.y,
                transform.position.z + (Random.Range(0, 100) > 50 ? Random.Range(-MinSpawnDistance, -MaxSpawnDistance) : Random.Range(MinSpawnDistance, MaxSpawnDistance)));

                var cloneObj = Instantiate(item.Object, SpawnPos, transform.rotation);
                cloneObj.transform.SetParent(gameObject.transform);
            }
        }
    }
}
