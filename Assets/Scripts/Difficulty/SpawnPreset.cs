using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Spawn Preset", menuName = "Scriptables/Spawn Preset", order = 2)]
public class SpawnPreset : ScriptableObject
{
    public string ObjectName;
    public GameObject Object;
    [Range(0, 100)] public float StartSpawnChance;
    [Range(0, 100)] public float MinSpawnChance;
    [Range(0,100)]public float MaxSpawnChance;
    [Range(0, 3)] public float ItemMultiplier;

    [Header("Current Game Spawn Value")]
    [Range(0, 100)] public float CurrentSpawn;
}