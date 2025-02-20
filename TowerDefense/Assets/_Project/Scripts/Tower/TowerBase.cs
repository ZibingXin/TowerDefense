using Unity.VisualScripting;
using UnityEngine;

namespace DoomsDayDefense
{
    public class TowerBase : MonoBehaviour
    {
        [SerializeField] private float range = 5f;
        [SerializeField] private float fireRate = 1f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        protected Transform target;
        [SerializeField] private float fireCountdown = 0f;

        private void Update()
        {
            UpdateTarget();
            if (target != null)
            {
                AimTarget(target.position);
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
            
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
                    float distance = col.GetComponentInParent<Enemy>().distance;
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = col.gameObject;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }

        }

        private void AimTarget(Vector3 position)
        {
            if (position == null) return;
            Vector3 dir = position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        protected virtual void Shoot() { }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
