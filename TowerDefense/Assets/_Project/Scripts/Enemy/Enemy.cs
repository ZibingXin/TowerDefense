using UnityEngine;
using UnityEngine.AI;

namespace DoomsDayDefense
{
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent agent;
        [SerializeField] private float health = 100f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float distanceThreshold = 1.0f;
        private WaypointSystem path;
        private int index = 0;
        private Vector3 destination;

        //private Transform Transform => GetComponent<Transform>();
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            path = FindAnyObjectByType<WaypointSystem>();
            destination = path.waypoints[index].position;
        }

        void Start()
        {
            agent.destination = destination;
            agent.speed = speed;
        }

        void Update()
        {
            if (Vector3.Distance(destination, transform.position) <= distanceThreshold)
            {
                index = (index + 1) % path.waypoints.Count;
                destination = path.waypoints[index].position;
                agent.destination = destination;
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
