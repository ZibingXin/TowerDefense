using UnityEngine;

namespace DoomsDayDefense
{
    public class TurretAI : MonoBehaviour
    {
        public Transform target;
        public float range;
        public float fireRate;
        private float fireCountdown;

        public GameObject bulletPrefab;

        void Update()
        {
            if(target == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, range);
                foreach(var collider in colliders)
                {
                    if(collider.CompareTag("Enemy"))
                    {
                        target = collider.transform;
                        break;
                    }
                }
            }

            if(target!= null)
            {
                if(Vector3.Distance(transform.position, target.position) > range)
                {
                    target = null;
                    return;
                }

                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

                if(fireCountdown <= 0)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }

        void Shoot()
        {
            GameObject bulletGO = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null) bullet.Seek(target);
        }
    }
}
