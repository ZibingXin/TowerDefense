using UnityEngine;

namespace DoomsDayDefense
{
    public class Cannonball : MonoBehaviour
    {
        public float speed = 10f;
        public int damage = 20;
        private Transform target;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            //Cannonballs are fired at the enemy in a parabolic trajectory
            Fire();
        }

        void Fire()
        {
            Vector3 dir = target.position - transform.position;
            dir.Normalize();
            dir *= speed;

            transform.position += dir * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                HitTarget();
            }
        }

        void HitTarget()
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
