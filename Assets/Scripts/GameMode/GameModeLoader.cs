using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeLoader : NetworkBehaviour
{
    private void Awake()
    {
        NetworkManager.SceneManager.OnSceneEvent += HandleMapSceneLoading;
    }

    private void OnEnable()
    {
        NetworkManager.SceneManager.OnSceneEvent += HandleMapSceneLoading;
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.SceneManager.OnSceneEvent += HandleMapSceneLoading;
        base.OnNetworkSpawn();
    }

    public override void OnDestroy()
    {
        NetworkManager.SceneManager.OnSceneEvent -= HandleMapSceneLoading;
        base.OnDestroy();
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.SceneManager.OnSceneEvent -= HandleMapSceneLoading;
        base.OnNetworkDespawn();
    }

    private void HandleMapSceneLoading(SceneEvent sceneEvent)
    {
        if (NetworkManager.LocalClientId != sceneEvent.ClientId) return;

        if (sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted)
        {
            StartCoroutine(MapSceneLoading());
        }
    }

    private IEnumerator MapSceneLoading()
    {
        yield return 0;
        NetworkManager.SceneManager.LoadScene(StaticGameModeSettings.MapName, LoadSceneMode.Additive);
        NetworkManager.SceneManager.OnSceneEvent -= HandleMapSceneLoading;
    }


}
