using UnityEngine;

namespace DoomsDayDefense
{
    [CreateAssetMenu(menuName = "Tower/New Blueprint")]
    public class TowerBlueprint : ScriptableObject
    {
        public string towerName;
        public int cost;
        public GameObject prefab;
        public Sprite icon;
        [TextArea] public string description;

        [Header("Upgrades")]
        public TowerBlueprint[] upgradePaths;
    }
}
