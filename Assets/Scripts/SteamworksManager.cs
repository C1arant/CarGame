using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteamworksManager : MonoBehaviour
{
    
    public SteamManager steamManager;
    void Awake()
    {
        if (steamManager == null)
        {
            steamManager = new SteamManager();
        }

        if (!SteamManager.Initialized)
        {
            Debug.Log("Steamworks failed to initialize!");
        }

        if (SteamManager.Initialized)
        {
            Debug.Log("Steamworks initialized!");
        }
    }
}
