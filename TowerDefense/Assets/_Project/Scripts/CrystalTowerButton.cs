/* File Name: CrystalTowerButton.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the crystal tower button.
 * 
 */

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
            // change button color when it is not interactable
            if (!GetComponent<Button>().interactable)
            {
                GetComponent<Image>().color = Color.gray;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }

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
