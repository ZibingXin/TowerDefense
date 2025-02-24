using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class ArcherTower : TowerBase
    {
        //public new int buildCost = 50;
        //protected override IEnumerator AttackRoutine()
        //{
        //    while (isActive)
        //    {
        //        UpdateTarget();
        //        if (target != null)
        //        {
        //            AimTarget(target.position);
        //            if (fireCountdown <= 0f)
        //            {
        //                Shoot();
        //                fireCountdown = 1f / fireRate;
        //            }
        //            fireCountdown -= Time.deltaTime;
        //        }
        //    }
        //    yield return new WaitForSeconds(fireRate);
        //}

        protected override void Shoot()
        {
            GameObject arrow = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().Seek(target); 
        }
    }
}
