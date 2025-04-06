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

        public GameObject Notice;
        public GameObject noticeText;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
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
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            pauseMenu.SetActive(true);
            canvasUI.SetActive(false);
            Time.timeScale = 0;
        }

        public void HidePauseMenu()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
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
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            if (Time.timeScale == 0) Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void SaveGame()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            SaveGameManager.Instance().SaveGame();
        }

        public void ShowNotice(string message)
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            Notice.SetActive(true);
            noticeText.GetComponent<TextMeshProUGUI>().text = message;
            Invoke(nameof(HideNotice), 2.5f);
        }

        public void HideNotice()
        {
            Notice.SetActive(false);
            noticeText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
