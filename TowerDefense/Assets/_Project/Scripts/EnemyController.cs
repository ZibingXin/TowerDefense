using UnityEngine;

namespace DoomsDayDefense
{
    public class EnemyController : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public int health = 100;
        public int goldReward = 10;

        private int currentWaypoint = 0;

        private void Update()
        {
            Vector3 targetPos = WaypointManager.Instance.GetWaypointPosition(currentWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                currentWaypoint++;
                if (currentWaypoint >= WaypointManager.Instance.waypoints.Length)
                {
                    GameManager.Instance.DamageBase(10);
                    Destroy(gameObject);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                GameManager.Instance.AddGold(goldReward);
                Destroy(gameObject);
            }
        }
    }
}
