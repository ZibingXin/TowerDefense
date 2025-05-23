/* File Name: CrystalPoints.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Enumerate the crystal type and manage the crystal points.
 * 
 */

using UnityEngine;
using UnityEngine.InputSystem;

namespace DoomsDayDefense
{
    public enum CrystalType
    {
        Red,
        Blue,
        Green
    }
    public class CrystalPoints : MonoBehaviour, IInteractable
    {
        public CrystalType crystalType;
        public GameObject crystalTowerPrefab;
        public Material normalEffect;
        public Material highlightEffect;
        private bool isActive = false;

        private bool tutorialBuild = true;

        private void OnMouseDown()
        {
            if (!isActive) return;
            if (GameManager.Instance.PurchaseTower(50))
            {
                BuildTower();
            }
        }

        public void OnTapped()
        {
            if (!isActive) return;
            if (GameManager.Instance.PurchaseTower(50))
            {
                BuildTower();
            }
        }

        public void ToggleHighlight()
        {
            // find the child object with the name "rocks", change its material to the highlight effect
            Transform rocks = transform.Find("rocks");
            rocks.GetComponent<MeshRenderer>().material = highlightEffect;
            isActive = true;
        }

        public void UnToggleHighlight()
        {
            Transform rocks = transform.Find("rocks");
            rocks.GetComponent<MeshRenderer>().material = normalEffect;
            isActive = false;
        }


        private void BuildTower()
        {
            if (tutorialBuild && TutorialUI.Instance.currentIndex == 0 )
            {
                TutorialUI.Instance.NextTutorial();
                tutorialBuild = false;
            }
            //Destroy(gameObject);
            gameObject.SetActive(false);
            GameObject tower = Instantiate(crystalTowerPrefab, transform.position, Quaternion.identity);
            CrystalCapturerTower towerScript = tower.GetComponent<CrystalCapturerTower>();
            towerScript.Initialize(crystalType);
            UnToggleHighlight();

            FindFirstObjectByType<CrystalTowerButton>().OnCancel();

            
        }


    }
}
