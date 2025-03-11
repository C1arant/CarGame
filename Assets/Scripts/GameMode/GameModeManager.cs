using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private GameModeScriptableObject[] _gameModes;

    [Header("GameMode Settings UI")]
    [SerializeField] private TMP_Dropdown _gameModeDropdown;
    [SerializeField] private TMP_Dropdown _gameMapsDropdown;

    private void OnEnable()
    {
        StaticGameModeSettings.Clear();
        StaticGameModeSettings.SetUp(this._gameModes[0], 0);

        if(LobbyManager.CurrentLobby.HostId != AuthenticationService.Instance.PlayerId)
        {
            this.gameObject.SetActive(false);
        }

        // GameMode Dropdown SetUp
        this._gameModeDropdown.options.Clear();
        foreach (GameModeScriptableObject gameMode in this._gameModes)
        {
            this._gameModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = gameMode.Name() });
        }
        UpdateMapsList(this._gameModes[this._gameModeDropdown.value]);


        // Game Maps Dropdown SetUp
        this._gameModeDropdown.onValueChanged.AddListener(delegate {
            Debug.Log("Loading new Maps");
            StaticGameModeSettings.SetGameMode(this._gameModes[this._gameModeDropdown.value]);
            UpdateMapsList(this._gameModes[this._gameModeDropdown.value]);
        });

        this._gameMapsDropdown.onValueChanged.AddListener(delegate
        {
            StaticGameModeSettings.SetMapIndex(this._gameMapsDropdown.value);
        });
    }

    private void UpdateMapsList(GameModeScriptableObject gameMode)
    {
        this._gameMapsDropdown.options.Clear();

        foreach (string t in gameMode.Maps())
        {
            this._gameMapsDropdown.options.Add(new TMP_Dropdown.OptionData() { text = t });
        }
    }

    private GameModeScriptableObject GetGameModeFromName(string name)
    {
        foreach (GameModeScriptableObject gameMode in this._gameModes)
        {
            if (gameMode.name == name) return gameMode;
        }

        return null;
    }
}
