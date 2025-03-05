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

        private int currentWaveIndex = -1;
        private bool isSpawning = false;
        private bool wavesStarted = false;

        public void StartWaves()
        {
            if (!isSpawning && currentWaveIndex < waves.Count - 1)
            {
                wavesStarted = true;
                StartNextWave();
            }
        }

        private void StartNextWave()
        {
            if (!isSpawning && currentWaveIndex < waves.Count - 1)
            {
                currentWaveIndex++;
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            }
        }

        IEnumerator SpawnWave(Wave wave)
        {
            isSpawning = true;

            Debug.Log("Spawning " + wave.waveName + " wave with " + wave.count + " enemies");

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab, wave.speedMultiplier);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            isSpawning = false;

            if (currentWaveIndex < waves.Count - 1)
            {
                yield return new WaitForSeconds(5f);
                StartNextWave();
            }
            else
            {
                Debug.Log("All waves have been completed");
            }
        }

        private void SpawnEnemy(GameObject enemyPrefab, float speedMultiplier)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
        }
    }
}
