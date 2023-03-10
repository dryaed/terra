using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerInventoryHolder primaryInventoryHolder = new();

    private PlayerMovement fpsController;

    private void Awake()
    {
        fpsController = GetComponent<PlayerMovement>();
        SaveLoad.OnLoadGame += LoadPlayerData;
        SaveGameManager.OnSaveGameData += SavePlayerData;
    }
    
    public void SavePlayerData() {
        var playerData = new PlayerData(transform.position, transform.rotation, primaryInventoryHolder.PrimaryInventorySystem);
        //Debug.Log("NIGGER " + transform.position + " " + transform.rotation + " ");
        SaveGameManager.data.playerData = playerData;
        //SaveGameManager.SaveData();
    }
    
       
       
    public void LoadPlayerData(SaveData data)
    {
        fpsController.enabled = false;

        transform.SetPositionAndRotation(data.playerData.PlayerPosition, data.playerData.PlayerRotation);

        Invoke(nameof(EnableController), 0.25f);
    }

    private void EnableController()
    {
        fpsController.enabled = true;
    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;

    public PlayerData(Vector3 _position, Quaternion _rotation, InventorySystem _invSystem)
    {
        PlayerPosition = _position;
        PlayerRotation = _rotation;
    }
}