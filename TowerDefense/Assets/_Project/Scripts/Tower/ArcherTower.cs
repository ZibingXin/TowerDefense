using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    public class ArcherTower : TowerBase
    {
        
        protected override void Shoot()
        {
            GameObject arrow = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().Seek(target); 
        }
    }
}
