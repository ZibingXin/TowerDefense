/* File Name: GameManager.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the game state.
 * 
 */

using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

namespace DoomsDayDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private int startingGold = 100;
        [SerializeField] private int startingHealth = 100;

        public int currentGold;
        public int currentHealth;

        public int redCrystals;
        public int blueCrystals;
        public int greenCrystals;

        public UIManager uiManager;
        public WaveSpawner waveSpawnerScript;

        private int NumberOfEnemy = int.MaxValue;
        private int EnemyKilled = -1;

        public int CurrentGold
        {
            get => currentGold;
            set
            {
                currentGold = Mathf.Max(0, value);
                OnGoldChanged?.Invoke(currentGold);
            }
        }

        public event Action<int> OnGoldChanged;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Level1")
            {
                StartCoroutine(LinkToComponents());
            }
        }

        private System.Collections.IEnumerator LinkToComponents()
        {
            yield return null;
            uiManager = GameObject.Find("UI").GetComponent<UIManager>();
            waveSpawnerScript = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
            Reset();
            uiManager.Instance.ShowNotice("Welcome!");
        }

        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
            DontDestroyOnLoad(gameObject);
            Reset();
        }

        public void Reset()
        {
            CurrentGold = startingGold;
            currentHealth = startingHealth;
            EnemyKilled = 0;
            if (waveSpawnerScript != null)
            {
                GetNumberOfEnemy();
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Level1");
        }

        private void Update()
        {
            if (EnemyKilled >= NumberOfEnemy)
            {
                uiManager.Instance.ShowWinMenu();
            }
            if (currentHealth <= 0)
            {
                //Time.timeScale = 0;
                uiManager.Instance.ShowGameOverMenu();
            }
            if (EnemyKilled == 1) { uiManager.Instance.ShowNotice("First enemy killed!"); }
            if (EnemyKilled == 10) { uiManager.Instance.ShowNotice("First wave cleaned!"); }
        }

        public bool PurchaseTower(int cost)
        {
            if (cost > CurrentGold) return false;
            CurrentGold -= cost;
            return true;
        }

        public void AddGold(int amount)
        {
            CurrentGold += amount;
            EnemyKilled++;
        }

        public void HitBase(int damage)
        {
            currentHealth -= damage;
            EnemyKilled++;
        }

        public void AddCrystal(CrystalType ct, int n)
        {
            switch (ct)
            {
                case CrystalType.Red:
                    if (redCrystals < 200) redCrystals += n;
                    if (redCrystals > 200) redCrystals = 200;
                    break;
                case CrystalType.Blue:
                    if (blueCrystals < 200) blueCrystals += n;
                    if (blueCrystals > 200) blueCrystals = 200;
                    break;
                case CrystalType.Green:
                    if (greenCrystals < 200) greenCrystals += n;
                    if (greenCrystals > 200) greenCrystals = 200;
                    break;
            }
        }

        public void DeductCrystal(CrystalType ct, int n)
        {
            switch (ct)
            {
                case CrystalType.Red:
                    if (redCrystals >= n) redCrystals -= n;
                    break;
                case CrystalType.Blue:
                    if (blueCrystals >= n) blueCrystals -= n;
                    break;
                case CrystalType.Green:
                    if (greenCrystals >= n) greenCrystals -= n;
                    break;
            }
        }

        private void GetNumberOfEnemy()
        {
            //GameObject waveSpawner = GameObject.Find("WaveSpawner");
            //WaveSpawner waveSpawnerScript = waveSpawner.GetComponent<WaveSpawner>();
            NumberOfEnemy = waveSpawnerScript.GetNumberOfEnemy();
        }

        public int[] GetSaveInts()
        {
            return new[] { currentGold, currentHealth, redCrystals, blueCrystals, greenCrystals };
        }

        public void LoadSaveInts(int[] savedInts)
        {
            currentGold = savedInts[0];
            currentHealth = savedInts[1];
            redCrystals = savedInts[2];
            blueCrystals = savedInts[3];
            greenCrystals = savedInts[4];

        }

        public string[] GetSaveStrings()
        {
            string[] towersInfo = new string[0];
            // find all towers in the scene
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            // get the tower info
            foreach (var tower in towers)
            {
                TowerBase towerScript = tower.GetComponent<TowerBase>();
                string towerInfo;
                switch (towerScript)
                {
                    case ArcherTower archerTower:
                        // 1,3.4,5.6,7.8
                        towerInfo =
                            "T,1," +
                            archerTower.level.ToString()+ "," +
                            archerTower.transform.position.x.ToString()+ "," +
                            archerTower.transform.position.y.ToString()+ "," +
                            archerTower.transform.position.z.ToString();
                        break;
                    case MachineGunTower machineGunTower:
                        towerInfo =
                            "T,2," +
                            machineGunTower.level.ToString() + "," +
                            machineGunTower.transform.position.x.ToString() + "," +
                            machineGunTower.transform.position.y.ToString() + "," +
                            machineGunTower.transform.position.z.ToString();
                        break;
                    case CatapultTower catapultTower:
                        towerInfo =
                            "T,3," +
                            catapultTower.level.ToString() + "," +
                            catapultTower.transform.position.x.ToString() + "," +
                            catapultTower.transform.position.y.ToString() + "," +
                            catapultTower.transform.position.z.ToString();
                        break;
                    case CannonTower cannonTower:
                        towerInfo =
                            "T,4," +
                            cannonTower.level.ToString() + "," +
                            cannonTower.transform.position.x.ToString() + "," +
                            cannonTower.transform.position.y.ToString() + "," +
                            cannonTower.transform.position.z.ToString();
                        break;
                    
                }
                Array.Resize(ref towersInfo, towersInfo.Length + 1);
            }

            GameObject[] crystalTowers = GameObject.FindGameObjectsWithTag("CrystalTower");
            foreach (var crystalTower in crystalTowers)
            {
                CrystalCapturerTower crystalTowerScript = crystalTower.GetComponent<CrystalCapturerTower>();
                Transform transform = crystalTower.transform;
                string towerInfo =
                    "C," +
                    crystalTowerScript.crystalType + "," +
                    transform.position.x + "," +
                    transform.position.y + "," +
                    transform.position.z;
                Array.Resize(ref towersInfo, towersInfo.Length + 1);
            }


            return towersInfo;
        }

        public void LoadSavedStrings(string[] savedStrings)
        {
            BuildManager buildManager = FindFirstObjectByType<BuildManager>();
            foreach (var towerInfo in savedStrings)
            {
                string[] info = towerInfo.Split(',');
                switch (info[0])
                {
                    case "T":
                        int towerType = int.Parse(info[1]);
                        int level = int.Parse(info[2]);
                        float x = float.Parse(info[3]);
                        float y = float.Parse(info[4]);
                        float z = float.Parse(info[5]);
                        Vector3 position = new Vector3(x, y, z);
                        switch (towerType)
                        {
                            case 1:
                                GameObject archerTower = Instantiate(buildManager.towers[0], position, Quaternion.identity) as GameObject;
                                archerTower.GetComponent<ArcherTower>().level = level;
                                break;
                            case 2:
                                GameObject machineGunTower = Instantiate(buildManager.towers[1], position, Quaternion.identity) as GameObject;
                                machineGunTower.GetComponent<MachineGunTower>().level = level;
                                break;
                            case 3:
                                GameObject catapultTower = Instantiate(buildManager.towers[2], position, Quaternion.identity) as GameObject;
                                catapultTower.GetComponent<CatapultTower>().level = level;
                                break;
                            case 4:
                                GameObject cannonTower = Instantiate(buildManager.towers[3], position, Quaternion.identity) as GameObject;
                                cannonTower.GetComponent<CannonTower>().level = level;
                                break;
                        }
                        break;
                    case "C":
                        CrystalType crystalType = (CrystalType)Enum.Parse(typeof(CrystalType), info[1]);
                        float x1 = float.Parse(info[2]);
                        float y1 = float.Parse(info[3]);
                        float z1 = float.Parse(info[4]);
                        Vector3 position1 = new Vector3(x1, y1, z1);
                        switch (crystalType)
                        {
                            case CrystalType.Red:
                                GameObject redCrystalTower = Instantiate(buildManager.towers[4], position1, Quaternion.identity) as GameObject;
                                redCrystalTower.GetComponent<CrystalCapturerTower>().Initialize(crystalType);
                                break;
                            case CrystalType.Green:
                                GameObject greenCrystalTower = Instantiate(buildManager.towers[5], position1, Quaternion.identity) as GameObject;
                                greenCrystalTower.GetComponent<CrystalCapturerTower>().Initialize(crystalType);
                                break;
                            case CrystalType.Blue:
                                GameObject blueCrystalTower = Instantiate(buildManager.towers[6], position1, Quaternion.identity) as GameObject;
                                blueCrystalTower.GetComponent<CrystalCapturerTower>().Initialize(crystalType);
                                break;
                        }
                        break;
                }
            }
        }


    }
}
