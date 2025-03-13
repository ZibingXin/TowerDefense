using System;
using UnityEngine;

namespace DoomsDayDefense
{
    public class Cannonball : MonoBehaviour
    {
        public float speed = 10f;
        public int damage = 20;
        private Transform target;

        private Vector3 initialVelocity;
        private Vector3 startPosition;
        private float timeSinceLaunch;
        internal Action<Vector3> OnImpact;

        public void Seek(Transform _target)
        {
            target = _target;
            startPosition = transform.position;
            initialVelocity = CalculateInitialVelocity();
            timeSinceLaunch = 0f;
        }

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            timeSinceLaunch += Time.deltaTime;
            Fire();
        }

        void Fire()
        {
            Vector3 currentPosition = startPosition + initialVelocity * timeSinceLaunch + 1.0f * Physics.gravity * timeSinceLaunch * timeSinceLaunch;
            transform.position = currentPosition;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                HitTarget();
            }
        }

        Vector3 CalculateInitialVelocity()
        {
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;
            float angle = 45f * Mathf.Deg2Rad; // Launch angle in radians

            float v0 = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));
            Vector3 velocity = new Vector3(direction.x, Mathf.Tan(angle) * distance, direction.z).normalized * v0;

            return velocity;
        }

        void HitTarget()
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
