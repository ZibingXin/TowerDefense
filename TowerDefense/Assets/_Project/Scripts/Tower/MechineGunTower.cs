using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class MachineGunTower : TowerBase
    {
        //public new int buildCost = 50;

        [Header("Machine Gun Settings")]
        public int burstCount = 5;
        public float burstInterval = 0.1f;

        //protected override IEnumerator AttackRoutine()
        //{
        //    throw new System.NotImplementedException();
        //}

        protected override void Shoot()
        {
            StartCoroutine(BurstFire());
        }

        IEnumerator BurstFire()
        {
            for (int i = 0; i < burstCount; i++)
            {
                InstantiateBullet();
                yield return new WaitForSeconds(burstInterval);
            }
        }

        void InstantiateBullet()
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().Seek(target);
        }
    }

}
