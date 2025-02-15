using UnityEngine;

namespace DoomsDayDefense
{
    public class Bullet : MonoBehaviour
    {
        private Transform target;
        public float speed = 10f;
        public int damage = 20;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float frameDistance = speed * Time.deltaTime;

            if(dir.magnitude <= frameDistance)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * frameDistance, Space.World);
        }

        void HitTarget()
        {
            EnemyHealth enemy = target.GetComponent<EnemyHealth>();
            if (enemy != null) enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
