using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField] private int _startingWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }
    
    private IEnumerator SpawnAllWaves()
    {
        for (int i = _startingWave; i < _waveConfigs.Count; i++)
        {
            WaveConfig currentWave = _waveConfigs[i];

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            GameObject newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
