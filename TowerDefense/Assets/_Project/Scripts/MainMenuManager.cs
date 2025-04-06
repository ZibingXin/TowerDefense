/* File Name: MainMenuManager.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the main menu functions.
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject settingPanel;
        //public AudioManager AudioManager;

        // mainmenu methods
        public void OnPlayButtonClicked()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            GameManager.Instance.StartGame();
        }

        public void OnLoadButtonClicked()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            //SceneManager.LoadScene("Level1");
            SaveGameManager.Instance().LoadGame();
        }

        public void OnSettingButtonClicked()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            settingPanel.SetActive(true);
        }

        public void OnQuitButtonClicked()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        // setting panel methods
        public void OnBackButtonClicked()
        {
            AudioManager.Instance.PlaySound(GameEvent.ButtonClicked);
            settingPanel.SetActive(false);
        }

       

        
    }
}
