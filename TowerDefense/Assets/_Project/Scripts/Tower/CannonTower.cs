/* File Name: CannonTower.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Cannon tower class that inherits from the TowerBase class.
 * 
 */

using UnityEngine;

namespace DoomsDayDefense
{
    public class CannonTower : TowerBase
    {

        [Header("Cannon Settings")]
        public float explosionRadius = 3f;
        public ParticleSystem explosionEffect;


        protected override void Shoot()
        {
            GameObject cannonball = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Cannonball cannonballComponent = cannonball.GetComponent<Cannonball>();
            if (cannonballComponent != null)
            {
                cannonballComponent.Seek(target);
                cannonballComponent.OnImpact += HandleExplosion;
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
