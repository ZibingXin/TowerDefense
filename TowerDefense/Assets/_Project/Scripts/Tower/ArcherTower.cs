using UnityEngine;

namespace DoomsDayDefense
{
    public class ArcherTower : TowerBase
    {
        protected override void Shoot()
        {
            GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().Seek(target);
        }
    }
}
