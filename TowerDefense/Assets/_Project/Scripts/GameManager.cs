using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        public GameObject pauseMenu;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance!= this)
            {
                Destroy(gameObject);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // pause game
            if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }

        }

        // pause menu button
        public void OnResumeButtonClicked()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

        public void OnQuitButtonClicked()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
