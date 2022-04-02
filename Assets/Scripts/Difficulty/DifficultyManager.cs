using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] public LevelSpawnData LevelSpawnables;
    [SerializeField] private float GameDistance;
    [SerializeField, Range(0, 10)] private float DifficultyMultiplier = 1;
    [SerializeField, Range(100, 10000)] private float DistanceScaler = 500;
    private float SpawnSum;
    private float fSpawnChance;

    // Start is called before the first frame update
    void Start()
    {
        if (LevelSpawnables == null)
            return;
        CalculateGameDifficulty();
    }


    public void CalculateGameDifficulty()
    {
        GameDistance = GameManager.Instance.CanvasController.distance;
        CalculateGroupOfItems(LevelSpawnables.SpawnableEnemies);
        CalculateGroupOfItems(LevelSpawnables.SpawnableObstacles);
        CalculateGroupOfItems(LevelSpawnables.SpawnablePickups);
    }

    void CalculateGroupOfItems(SpawnPreset[] Items)
    {
        foreach(SpawnPreset preset in Items)
        {
            preset.CurrentSpawn = CalculateSpawnChance(preset);
        }
        //Debug.Log(preset.name + " :" + preset.CurrentSpawn);
    }

    float CalculateSpawnChance(SpawnPreset preset)
    {
        SpawnSum = ((GameDistance / DistanceScaler) *  preset.ItemMultiplier) * DifficultyMultiplier;
        fSpawnChance = preset.StartSpawnChance + SpawnSum;
        if(fSpawnChance <= preset.MaxSpawnChance)
        {
            return fSpawnChance;
        }
        else
        {
            return 100f;
        }
        
    }
}
