using System;
using UnityEngine;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private int startingGold = 50;
        [SerializeField] private int startingHealth = 100;
        [SerializeField] private int startingWave = 1;
        public int currentGold;
        public int currentHealth;

        public int redCrystals;
        public int blueCrystals;
        public int greenCrystals;

        [SerializeField] private UIManager uiManager;

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
        }

        private void Update()
        {
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
        }

        public void HitBase(int damage)
        {
            currentHealth -= damage;
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

    }
}
