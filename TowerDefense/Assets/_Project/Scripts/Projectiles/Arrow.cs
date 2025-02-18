using UnityEngine;

namespace DoomsDayDefense
{
    public class Arrow : MonoBehaviour
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

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
        }

        void HitTarget()
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
