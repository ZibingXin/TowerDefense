/* File Name: GameManager.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the game state.
 * 
 */

using System;
using UnityEngine;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private int startingGold = 100;
        [SerializeField] private int startingHealth = 100;

        public int currentGold;
        public int currentHealth;

        public int redCrystals;
        public int blueCrystals;
        public int greenCrystals;

        [SerializeField] private UIManager uiManager;

        private int NumberOfEnemy;
        private int EnemyKilled;

        public int CurrentGold
        {
            get => currentGold;
            set
            {
                currentGold = Mathf.Max(0, value);
                OnGoldChanged?.Invoke(currentGold);
            }
        }

        public event Action<int> OnGoldChanged;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            CurrentGold = startingGold;
            currentHealth = startingHealth;
            EnemyKilled = 0;
            GetNumberOfEnemy();
        }

        private void Update()
        {
            if (EnemyKilled >= NumberOfEnemy)
            {
                uiManager.ShowWinMenu();
            }
            if (currentHealth <= 0)
            {
                //Time.timeScale = 0;
                uiManager.ShowGameOverMenu();
            }
        }

        public bool PurchaseTower(int cost)
        {
            if (cost > CurrentGold) return false;
            CurrentGold -= cost;
            return true;
        }

        public void AddGold(int amount)
        {
            CurrentGold += amount;
            EnemyKilled++;
        }

        public void HitBase(int damage)
        {
            currentHealth -= damage;
            EnemyKilled++;
        }

        public void AddCrystal(CrystalType ct, int n)
        {
            switch (ct)
            {
                case CrystalType.Red:
                    if (redCrystals < 200) redCrystals += n;
                    if (redCrystals > 200) redCrystals = 200;
                    break;
                case CrystalType.Blue:
                    if (blueCrystals < 200) blueCrystals += n;
                    if (blueCrystals > 200) blueCrystals = 200;
                    break;
                case CrystalType.Green:
                    if (greenCrystals < 200) greenCrystals += n;
                    if (greenCrystals > 200) greenCrystals = 200;
                    break;
            }
        }

        public void DeductCrystal(CrystalType ct, int n)
        {
            switch (ct)
            {
                case CrystalType.Red:
                    if (redCrystals >= n) redCrystals -= n;
                    break;
                case CrystalType.Blue:
                    if (blueCrystals >= n) blueCrystals -= n;
                    break;
                case CrystalType.Green:
                    if (greenCrystals >= n) greenCrystals -= n;
                    break;
            }
        }

        private void GetNumberOfEnemy()
        {
            GameObject waveSpawner = GameObject.Find("WaveSpawner");
            WaveSpawner waveSpawnerScript = waveSpawner.GetComponent<WaveSpawner>();
            NumberOfEnemy = waveSpawnerScript.GetNumberOfEnemy();
        }

    }
}
