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
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
                if (pauseMenu.activeSelf)
                {
                    HidePauseMenu();
                }
                else
                {
                    ShowPauseMenu();
                }
            }

            healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + GameManager.Instance.currentHealth;
            goldText.GetComponent<TextMeshProUGUI>().text = "Gold: " + GameManager.Instance.currentGold;
            redCrystalText.GetComponent<TextMeshProUGUI>().text = "Red: " + GameManager.Instance.redCrystals + "/200";
            greenCrystalText.GetComponent<TextMeshProUGUI>().text = "Green: " + GameManager.Instance.greenCrystals + "/200";
            blueCrystalText.GetComponent<TextMeshProUGUI>().text = "Blue: " + GameManager.Instance.blueCrystals + "/200";
        }

        public void ShowPauseMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void HidePauseMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        public void ShowGameOverMenu()
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void GoBackToMainMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            if (Time.timeScale == 0) Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        
    }
}
