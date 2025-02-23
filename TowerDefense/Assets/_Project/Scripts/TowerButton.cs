using UnityEngine;

namespace DoomsDayDefense
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private int towerIndex;

        public void OnClick()
        {
            BuildManager.Instance.SelectTower(towerIndex);
        }
    }
}
