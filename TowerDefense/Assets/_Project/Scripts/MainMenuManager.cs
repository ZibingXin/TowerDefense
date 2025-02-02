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
            SceneManager.LoadScene("Level1");
        }

        public void OnSettingButtonClicked()
        {
            settingPanel.SetActive(true);
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        // setting panel methods
        public void OnBackButtonClicked()
        {
            settingPanel.SetActive(false);
        }

        //public void OnMusicSliderChanged(float value)
        //{
        //    AudioManager.Instance.SetMusicVolume(value);
        //}



        
    }
}
