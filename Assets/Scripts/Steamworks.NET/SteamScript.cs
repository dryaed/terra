using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;

public class SteamScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI helloText;

    private void Start()
    {
        
        if (!SteamManager.Initialized) return;
        var personaName = SteamFriends.GetPersonaName();
        Debug.Log(personaName);
        helloText.text = $"Hello, {personaName}";
    }
}
