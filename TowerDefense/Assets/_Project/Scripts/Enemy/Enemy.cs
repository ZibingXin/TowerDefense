using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace DoomsDayDefense
{
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent agent;
        [SerializeField] private float health = 100f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float distanceThreshold = 1.0f;

        [SerializeField] private int goldReward = 10;

        private WaypointSystem path;
        private int index = 0;
        private Vector3 destination;

        public float distance;

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
                if (index < path.waypoints.Count - 1) 
                { 
                    index++;
                    destination = path.waypoints[index].position;
                    agent.destination = destination;
                }
                else
                {
                    HitBase();
                }
            }
            distance = SumDistance(index);
        }

        public float SumDistance(int index)
        {
            // sum the distance from the current position to the all next waypoints
            float sum = Vector3.Distance(transform.position, path.waypoints[index].position);
            for (int i = index + 1; i < path.waypoints.Count - 1; i++)
            {
                sum += Vector3.Distance(path.waypoints[i].position, path.waypoints[i + 1].position);
            }
            return sum;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
                FindAnyObjectByType<GameManager>().AddGold(goldReward);
            }
        }

        public void HitBase()
        {
            Debug.Log("Enemy hit base");
            Destroy(gameObject);
        }
    }
}
