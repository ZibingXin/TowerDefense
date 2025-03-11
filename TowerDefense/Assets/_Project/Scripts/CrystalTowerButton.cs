using UnityEngine;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class CrystalTowerButton : MonoBehaviour
    {
        public CrystalPoints[] crystalPoints;

        private void Update()
        {
            GetComponent<Button>().interactable = GameManager.Instance.currentGold >= 50;
            if (Input.GetMouseButtonDown(1))
            {
                foreach (var cp in crystalPoints)
                {
                    cp.UnToggleHighlight();
                }
            }
        }
        public void OnClick()
        {
            foreach(var cp in crystalPoints)
            {
                cp.ToggleHighlight();
            }
        }
    }
}
