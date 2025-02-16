using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        public GameObject pauseMenu;

        public int gold;
        public int lives;


        void Awake()
        {
            Instance = this;
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

        public void DamageBase(int damage)
        {
            lives -= damage;
            if (lives <= 0) GameOver();
        }

        public void AddGold(int goldReward)
        {
            this.gold += goldReward;
        }

        void GameOver()
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
}
