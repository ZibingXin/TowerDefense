using UnityEngine;

namespace DoomsDayDefense
{
    public class TowerBase : MonoBehaviour
    {
        public float range = 5f;
        public float fireRate = 1f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        protected Transform target;
        private float fireCountdown = 0f;

        private void Update()
        {
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = col.gameObject;
                    }
                }
            }

            if (nearestEnemy!= null) target = nearestEnemy.transform;
        }

        protected virtual void Shoot() { }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
