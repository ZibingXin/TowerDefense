using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    public class UIManager : MonoBehaviour
    {
        public UIManager Instance;

        public GameObject pauseMenu;
        public GameObject gameOverMenu;

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
                if (pauseMenu.activeSelf)
                {
                    HidePauseMenu();
                }
                else
                {
                    ShowPauseMenu();
                }
            }
        }

        public void ShowPauseMenu()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void HidePauseMenu()
        {
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
            if (Time.timeScale == 0) Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
