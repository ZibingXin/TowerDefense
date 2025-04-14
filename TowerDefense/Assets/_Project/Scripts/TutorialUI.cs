using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class TutorialUI : MonoBehaviour
    {
        public static TutorialUI Instance;

        [SerializeField] GameObject tutorialPanel;
        [SerializeField] GameObject tutorialText;
        //[SerializeField] GameObject tutorialSlider;

        //[SerializeField] GameObject[] pointerPositions;

        public int currentIndex = 0;
        int enemyKills = 0;

        List<string> tutorialTexts = new() { 
            "Build a crystal tower",
            "Build a archer tower",
            "Build a mechine gun tower",
            "Start the wave",
            "Upgrade the tower",
            "Kill 10 enemies"
        };

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            currentIndex = 0;
            tutorialPanel.SetActive(true);
            tutorialText.SetActive(true);
            //tutorialSlider.SetActive(false);
            tutorialText.GetComponent<TextMeshProUGUI>().SetText(tutorialTexts[currentIndex]);
        }

        private void FixedUpdate()
        {
            enemyKills = GameManager.Instance.GetEnemyKills();
            if (enemyKills >= 10 && currentIndex == 5)
            {
                NextTutorial();
            }
        }

        public void NextTutorial()
        {
            //pointerPositions[currentIndex].SetActive(false);
            currentIndex++;
            if (currentIndex >= tutorialTexts.Count)
            {
                tutorialPanel.SetActive(false);
                return;
            }
            tutorialText.GetComponent<TextMeshProUGUI>().SetText(tutorialTexts[currentIndex]);


        }

    }
}
