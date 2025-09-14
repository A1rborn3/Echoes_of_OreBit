using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string currentSlot = "Save0"; //this is default. can be changed to support multiple files

    public void SaveGame()
    {
        SaveData data = new SaveData(); //creates new data to hold all the new save data

        foreach (var saveable in UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
        {
            if (saveable is ISaveable s) 
                s.Save(data);
        }

        SaveManager.SaveGame(data, currentSlot);
    }

    public void LoadGame()
    {
        SaveData data = SaveManager.LoadGame(currentSlot);

        foreach (var saveable in UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
        {
            if (saveable is ISaveable s)
                s.Load(data);
        }
    }
}