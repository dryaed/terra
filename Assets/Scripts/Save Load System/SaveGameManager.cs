using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveData data;
    //PlayerSaveData playerSaveData = new PlayerSaveData();

    private void Awake()
    {
        data = new SaveData();
        SaveLoad.OnLoadGame += LoadData;
        //SaveLoad.OnSaveGame += SaveData;
    }

    public static void SaveData()
    {
        var saveData = data;
        //playerSaveData.SavePlayerData();
        SaveLoad.Save(saveData);
    }

    public static void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void TryLoadData()
    {
        SaveLoad.Load();
    }

    public void DeleteData()
    {
        SaveLoad.Delete();
    }
}
