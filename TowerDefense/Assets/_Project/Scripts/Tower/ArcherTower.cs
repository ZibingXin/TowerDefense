/* File Name: ArcherTower.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Archer tower class that inherits from the TowerBase class.
 * 
 */

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
