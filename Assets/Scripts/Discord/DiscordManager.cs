using UnityEngine;
using Discord;
using System;

public class DiscordManager : MonoBehaviour
{
    public static DiscordManager Instance { get; private set; }
    private Discord.Discord _discord;
    private ActivityManager _activityManager;
    private const long CLIENT_ID = 1348798183916175410;
    private long _startTime;


    private void Awake()
    {
        if (DiscordManager.Instance != null) Destroy(this.gameObject);

        Instance = this;
        this._discord = new Discord.Discord(CLIENT_ID, (UInt64)CreateFlags.Default);
        this._activityManager = this._discord.GetActivityManager();
        this._startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        this._discord.RunCallbacks();
    }

    private void OnDestroy()
    {
        this._discord.Dispose();
    }

    public void SetNewActivity(DiscordRichPresence drpData)
    {
        Activity newActivity = new Activity
        {
            State = drpData.StateName,
            Details = drpData.Details,
            Timestamps =
            {
                Start = (Int64)this._startTime,
            },
            Assets =
            {
                LargeImage = drpData.LargeArtwork,
                LargeText = drpData.LargeText,
                SmallImage = drpData.SmallArtwork,
                SmallText = drpData.SmallText,
            },
            Instance = drpData.Instance,
        };

        this._activityManager.UpdateActivity(newActivity, (result) =>
        {
            if(result == Discord.Result.Ok)
            {
                Debug.Log("OK");
            } else
            {
                Debug.Log("ERR");
            }
        });
    }
}
