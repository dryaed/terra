using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SaveGameManager : MonoBehaviour
{
    public static UnityAction OnSaveGameData;

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
        OnSaveGameData.Invoke();

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
