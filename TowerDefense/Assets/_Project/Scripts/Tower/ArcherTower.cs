using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class ArcherTower : TowerBase
    {
        //public new int buildCost = 50;
        protected override IEnumerator AttackRoutine()
        {
            throw new System.NotImplementedException();
        }

        protected override void Shoot()
        {
            GameObject arrow = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().Seek(target); 
        }
    }
}
