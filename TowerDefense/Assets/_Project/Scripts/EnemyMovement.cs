using UnityEngine;

namespace DoomsDayDefense
{
    public class EnemyMovement : MonoBehaviour
    {
        public Transform[] waypoints;
        private int currentWaypoint = 0;
        public float moveSpeed = 3f;

        void Update()
        {
            if (currentWaypoint < waypoints.Length)
            {
                Vector3 dir = waypoints[currentWaypoint].position - transform.position;
                transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

                if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                GameManager.instance.TakeDamage(10);
                Destroy(gameObject);
            }
        }
    }
}
