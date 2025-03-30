using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    [System.Serializable]
    public class SaveData
    {
        public int waveIndex;
        public int[] ints; // gold, health, red, green, blue
        public string[] towerInfo; // tower instance, tower position, tower level
    }


    [System.Serializable]
    public class SaveGameManager
    {
        private static SaveGameManager instance = null;

        private SaveGameManager() { }

        public static SaveGameManager Instance()
        {
            return instance ??= new SaveGameManager();
        }

        public void SaveGame()
        {
            var saveData = GetSaveData();
            var file = File.Create(Application.persistentDataPath + "/saveData.json");
            var writer = new StreamWriter(file);
            writer.Write(JsonUtility.ToJson(saveData));
            writer.Close();
            file.Close();
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Level1");
            GameManager.Instance.Reset();
            if (!File.Exists(Application.persistentDataPath + "/saveData.json")) return;
            var saveData = new SaveData();
            var reader = new StreamReader(Application.persistentDataPath + "/saveData.json");
            JsonUtility.FromJsonOverwrite(reader.ReadToEnd(), saveData);
            reader.Close();
            WaveSpawner.Instance.SetCurrentWaveIndex(saveData.waveIndex);
            GameManager.Instance.LoadSaveInts(saveData.ints);
            GameManager.Instance.LoadSavedStrings(saveData.towerInfo);
        }

        private SaveData GetSaveData()
        {
            var saveData = new SaveData();
            saveData.waveIndex = WaveSpawner.Instance.GetCurrentWaveIndex();
            saveData.ints = GameManager.Instance.GetSaveInts();
            return null;
        }

    }
}
