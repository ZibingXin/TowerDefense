/* File Name: UIManager.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the UI elements in the game.
 * 
 */


using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    public class UIManager : MonoBehaviour
    {
        public UIManager Instance;

        public GameObject pauseMenu;
        public GameObject gameOverMenu;
        public GameObject winMenu;
        public GameObject canvasUI;

        public GameObject healthText;
        public GameObject goldText;
        public GameObject redCrystalText;
        public GameObject greenCrystalText;
        public GameObject blueCrystalText;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            winMenu.SetActive(false);
            canvasUI.SetActive(true);
        }
        private void Update()
        {
            healthText.GetComponent<TextMeshProUGUI>().text = " " + GameManager.Instance.currentHealth;
            goldText.GetComponent<TextMeshProUGUI>().text = " " + GameManager.Instance.currentGold;
            redCrystalText.GetComponent<TextMeshProUGUI>().text = " " + GameManager.Instance.redCrystals + "/200";
            greenCrystalText.GetComponent<TextMeshProUGUI>().text = " " + GameManager.Instance.greenCrystals + "/200";
            blueCrystalText.GetComponent<TextMeshProUGUI>().text = " " + GameManager.Instance.blueCrystals + "/200";
        }

        public void ShowPauseMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            pauseMenu.SetActive(true);
            canvasUI.SetActive(false);
            Time.timeScale = 0;
        }

        public void HidePauseMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            pauseMenu.SetActive(false);
            canvasUI.SetActive(true);
            Time.timeScale = 1;
        }

        public void ShowGameOverMenu()
        {
            gameOverMenu.SetActive(true);
            canvasUI.SetActive(false);
            Time.timeScale = 0;
        }

        public void ShowWinMenu()
        {
            winMenu.SetActive(true);
            canvasUI.SetActive(false);
            Time.timeScale = 0;
        }

        public void GoBackToMainMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            if (Time.timeScale == 0) Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void SaveGame()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            SaveGameManager.Instance().SaveGame();
        }

    }
}
