using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        public GameObject turretPrefab;
        public GameObject buildEffect;

        private void Awake()
        {
            Instance = this;
        }

        public void BuildTurretOn(Vector3 position)
        {
            if(GameManager.Instance.gold >= 100)
            {
                Instantiate(turretPrefab, position, Quaternion.identity);
                Instantiate(buildEffect, position, Quaternion.identity);
                GameManager.Instance.gold -= 100;
            }
        }
    }
}
