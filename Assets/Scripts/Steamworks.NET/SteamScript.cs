using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour
{
    private void Start()
    {
        if (!SteamManager.Initialized) return;
        var personaName = SteamFriends.GetPersonaName();
        Debug.Log(personaName);
    }
}
