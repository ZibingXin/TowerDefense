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
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            SceneManager.LoadScene("Level1");
            GameManager.Instance.Reset();
        }

        public void OnLoadButtonClicked()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            //SceneManager.LoadScene("Level1");
            SaveGameManager.Instance().LoadGame();
        }

        public void OnSettingButtonClicked()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            settingPanel.SetActive(true);
        }

        public void OnQuitButtonClicked()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        // setting panel methods
        public void OnBackButtonClicked()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            settingPanel.SetActive(false);
        }

       

        
    }
}
