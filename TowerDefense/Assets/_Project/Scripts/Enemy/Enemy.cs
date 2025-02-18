using UnityEngine;
using UnityEngine.AI;

namespace DoomsDayDefense
{
    public class Enemy : MonoBehaviour
    {
        public float health = 100f;
        public float speed = 3f;
        private WaypointSystem path;
        private int currentWaypoint = 0;

        private Transform Transform => GetComponent<Transform>();

        void Start()
        {
            path = FindObjectOfType<WaypointSystem>();
            GetComponent<NavMeshAgent>().speed = speed;
        }

        void Update()
        {
            if (currentWaypoint < path.waypoints.Count)
            {
                GetComponent<NavMeshAgent>().SetDestination(path.waypoints[currentWaypoint].position);

                if (Vector3.Distance(Transform.position, path.waypoints[currentWaypoint].position) < 0.5f)
                {
                    currentWaypoint++;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
