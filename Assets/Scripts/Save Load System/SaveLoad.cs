using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public static class SaveLoad
{
    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;
    public static UnityAction OnLoadGameEvent;

    private static string saveDirectory = "/GameSaves/";
    private static string fileName = "GameSave.ngr";

    public static bool Save(SaveData data)
    {
        OnSaveGame?.Invoke();

        string dir = Application.persistentDataPath + saveDirectory;

        Delete();

        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dir + fileName, json); // currently writes to a static file name
        Debug.Log("Directory: "+dir);

        GUIUtility.systemCopyBuffer = dir;
        return true;
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + saveDirectory + fileName;
        SaveData data = new();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath); // currently reads from a static file name
            data = JsonUtility.FromJson<SaveData>(json);

            OnLoadGame?.Invoke(data);
            OnLoadGameEvent.Invoke();
        }
        else
        {
            Debug.LogError("Save File does not exist.");
        }

        return data;
    }

    public static void Delete()
    {
        string fullPath = Application.persistentDataPath + saveDirectory + fileName;
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}
