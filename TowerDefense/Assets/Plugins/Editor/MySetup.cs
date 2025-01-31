using System.IO;
using UnityEngine;
using UnityEditor;

public static class MySetup
{
    [MenuItem("Setup/Create Folders")]
    public static void CreateMyFolder()
    {
        Folder.CreateFolder("_Project", "Animations", "Art", "Audio", "Fonts",
        "Materials", "Prefabs", "ScriptableObjects", "Scripts", "Settings");
        AssetDatabase.Refresh();
    }
    static class Folder
    {
        public static void CreateFolder(string root, params string[] folders)
        {
            var fullPath = Path.Combine(Application.dataPath, root);
            foreach (string folder in folders)
            {
                var folderPath = Path.Combine(fullPath, folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
        }
    }
}
