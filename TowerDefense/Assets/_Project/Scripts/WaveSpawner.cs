using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoomsDayDefense
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject enemyPrefab;
        public int count;
        public float spawnInterval;
        public float speedMultiplier = 1f;
        public int goldRewardPerKill = 10;
    }

    public class WaveSpawner : MonoBehaviour
    {
        public List<Wave> waves;
        public Transform spawnPoint;

        private int currentWaveIndex = 0;
        private bool isSpawning = false;

        private void Update()
        {
            if (!isSpawning && currentWaveIndex < waves.Count)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            }
        }

        IEnumerator SpawnWave(Wave wave)
        {
            isSpawning = true;

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab, wave.speedMultiplier);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            isSpawning = false;
            currentWaveIndex++;
        }

        private void SpawnEnemy(GameObject enemyPrefab, float speedMultiplier)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
        }
    }
}
