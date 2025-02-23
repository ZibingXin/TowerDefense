using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class CannonTower : TowerBase
    {
        //public new int buildCost = 50;

        [Header("Cannon Settings")]
        public float explosionRadius = 3f;
        public ParticleSystem explosionEffect;

        protected override IEnumerator AttackRoutine()
        {
            throw new System.NotImplementedException();
        }

        protected override void Shoot()
        {
            GameObject cannonball = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            cannonball.GetComponent<Cannonball>().Seek(target);
            //cannonball.OnImpact += HandleExplosion;
        }

        void HandleExplosion(Vector3 explosionPosition)
        {
            explosionEffect.transform.position = explosionPosition;
            explosionEffect.Play();

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach (Collider col in colliders)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(0);
                }
            }
        }
    }

}
