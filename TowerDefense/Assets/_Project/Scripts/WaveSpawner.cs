using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class WaveSpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public float timeBetweenWaves = 5f;

        private int waveIndex = 0;
        private float countdown = 2f;

        void Update()
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
            countdown -= Time.deltaTime;
        }

        IEnumerator SpawnWave()
        {
            waveIndex++;
            for(int i = 0; i < waveIndex * 3; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }
        }

        void SpawnEnemy()
        {
            GameObject enemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().waypoints = waypoints;
        }
    }
}
