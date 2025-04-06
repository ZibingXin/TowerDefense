using UnityEngine;

namespace DoomsDayDefense
{
    public enum GameEvent
    {
        ButtonClicked,
        GameOver,
        GameWin,
        WaveStart,
        WaveEnd,
        EnemySpawned,
        EnemyDefeated,
        EnemyHit,
        TowerPlaced,
        TowerAttacked,
        TowerUpgraded,
        TowerSold,
        BaseDamaged
    }

    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        public delegate void GameEventHandler(GameEvent gameEvent, object data = null);
        public event GameEventHandler OnGameEvent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void TriggerEvent(GameEvent gameEvent, object data = null)
        {
            OnGameEvent?.Invoke(gameEvent, data);
        }
    }
}
