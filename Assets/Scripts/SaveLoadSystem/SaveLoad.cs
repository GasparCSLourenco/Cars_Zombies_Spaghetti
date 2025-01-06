using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public static class SaveLoad
{
    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;

    public static string directory = "/SaveData/";
    private static string fileName = "SaveGame.sav";

    public static bool Save(SaveData data)
    {
        OnSaveGame?.Invoke();
        

        string dir = Application.persistentDataPath + directory;
		GUIUtility.systemCopyBuffer = dir;

		if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir) ;
        }


		string json = JsonUtility.ToJson(data, true);
        
        Debug.Log(json);

        File.WriteAllText(dir + fileName, json);

        Debug.Log("Saving game");
        
        return true;
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveData saveData = new SaveData();

        if (File.Exists(fullPath))
        {
            Debug.Log("exists: " + fullPath);
            string json = File.ReadAllText(fullPath);
            saveData = JsonUtility.FromJson<SaveData>(json);

            OnLoadGame?.Invoke(saveData);
        }
        else
        {
            Debug.Log("Save file does not exist");
        }

        return saveData;
    }

	public static void DeleteSaveData()
	{
		string fullPath = Application.persistentDataPath + directory + fileName;
        if(File.Exists(fullPath)) File.Delete(fullPath);
	}
}
