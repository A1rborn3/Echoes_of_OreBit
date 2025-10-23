using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string folderPath = Application.persistentDataPath + "/saves/";

    public static void SaveGame(SaveData data, string slotName)
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string path = folderPath + slotName + ".json";
        string json = JsonUtility.ToJson(data, true); //converts save object into JSON string
        File.WriteAllText(path, json);//writes it all to teh save file

        Debug.Log("Game saved to " + path);
    }

    public static SaveData LoadGame(string slotName)
    {
        string path = folderPath + slotName + ".json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found in slot: " + slotName);
            return new SaveData(); // default blank save
        }

        string json = File.ReadAllText(path); //reads the full JSON file
        return JsonUtility.FromJson<SaveData>(json); //converts the above string into SAveData object for game use
    }

    public static string[] ListSaves()
    {
        if (!Directory.Exists(folderPath)) return new string[0];
        string[] files = Directory.GetFiles(folderPath, "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = Path.GetFileNameWithoutExtension(files[i]); //prints all save names into a list, will be used for menu when selecting save
        }
        return files;
    }

    public static void DeleteSave(string slotName)
    {
        string path = folderPath + slotName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log(slotName + " Save file deleted.");
        }
    }
}