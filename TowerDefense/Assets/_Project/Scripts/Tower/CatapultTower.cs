using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class CatapultTower : TowerBase
    {
        //public new int buildCost = 50;

        [Header("Catapult Settings")]
        public float explosionRadius = 3f;
        public ParticleSystem explosionEffect;

        //protected override IEnumerator AttackRoutine()
        //{
        //    throw new System.NotImplementedException();
        //}

        protected override void Shoot()
        {
            GameObject stone = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            stone.GetComponent<Stone>().Seek(target);
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
