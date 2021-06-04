using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _timeBetweenSpawns = 0.5f;
    [SerializeField] private float _spawnRandomFactor = 0.3f;
    [SerializeField] private int _numberOfEnemies = 5;
    [SerializeField] private float _movementSpeed = 2f;

    public GameObject GetEnemyPrefab() => _enemyPrefab;
    public GameObject GetPathPrefab() => _pathPrefab;
    public float GetTimeBetweenSpawns() => _timeBetweenSpawns;
    public float GetSpawnRandomFactor() => _spawnRandomFactor;
    public int GetNumberOfEnemies() => _numberOfEnemies;
    public float GetMovementSpeed() => _movementSpeed;
}
