/* File Name: CatapultTower.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Catapult tower class that inherits from the TowerBase class.
 * 
 */

using UnityEngine;

namespace DoomsDayDefense
{
    public class CatapultTower : TowerBase
    {
        [Header("Catapult Settings")]
        public float explosionRadius = 3f;
        public ParticleSystem explosionEffect;

        protected override void Shoot()
        {
            GameObject stone = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Stone stoneComponent = stone.GetComponent<Stone>();
            if (stoneComponent != null)
            {
                stoneComponent.Seek(target);
                stoneComponent.OnImpact += HandleExplosion;
            }
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
