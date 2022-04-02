using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LevelSpawnData", menuName = "Scriptables/LevelSpawnData", order = 3)]
public class LevelSpawnData : ScriptableObject
{
    public SpawnPreset[] SpawnablePickups;
    public SpawnPreset[] SpawnableObstacles;
    public SpawnPreset[] SpawnableEnemies;
}