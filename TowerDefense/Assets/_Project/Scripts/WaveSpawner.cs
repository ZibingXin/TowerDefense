/* File Name: WaveSpawner.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Wave class to store the information of each wave.
 * Spawn the enemies in the wave.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public TextMeshProUGUI waveText;

        private int currentWaveIndex = -1;
        private bool isSpawning = false;

        public void StartWaves()
        {
            if (!isSpawning && currentWaveIndex < waves.Count - 1)
            {
                StartNextWave();
            }
        }

        private void StartNextWave()
        {
            if (!isSpawning && currentWaveIndex < waves.Count - 1)
            {
                currentWaveIndex++;
                waveText.text = $"Wave {currentWaveIndex + 1}";
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
                for (int i = 5; i > 0; i--)
                {
                    waveText.text = "Wait " + i + "s..";
                    yield return new WaitForSeconds(1f);
                }
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

        public int GetNumberOfEnemy()
        {
            int result = 0;
            foreach (Wave wave in waves)
            {
                result += wave.count;
            }
            return result;
        }
    }
}
