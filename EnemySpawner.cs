using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigList;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        int size = waveConfigList.Count;
        int waveIndex = UnityEngine.Random.Range(0, size);
        for(waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex = UnityEngine.Random.Range(0, size))
        {
            var currentWave = waveConfigList[waveIndex];
            yield return StartCoroutine(SpawnAlllEnemiesInWave(currentWave));

        }
    }

    private IEnumerator SpawnAlllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemies = 0; enemies < waveConfig.GetNumberOfEnemies(); enemies++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
