using UnityEngine;

public class DrpUpdater : MonoBehaviour
{
    [SerializeField] private DiscordRichPresence _drpData;

    private void Awake()
    {
        Debug.Log("TEST");
        DiscordManager.Instance.SetNewActivity(_drpData);
    }
}
