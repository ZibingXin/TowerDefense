using System;
using UnityEngine;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private int startingGold = 200;
        [SerializeField] private int startingHealth = 100;
        public int currentGold;
        public int currentHealth;

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

    }
}
