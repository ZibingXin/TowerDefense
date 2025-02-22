using System;
using UnityEngine;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private int startingGold = 200;
        private int currentGold;

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

    }
}
