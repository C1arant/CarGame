using UnityEngine;

[CreateAssetMenu(fileName = "DiscordRichPresence", menuName = "Scriptable Objects/DiscordRichPresence")]
public class DiscordRichPresence : ScriptableObject
{
    public string StateName { get; private set; }
    public string Details { get; private set; }
    public string LargeArtwork { get; private set; }
    public string LargeText { get; private set; }
    public string SmallArtwork { get; private set; }
    public string SmallText { get; private set; }
    public bool Instance { get; private set; }

    [SerializeField] private string _stateName;
    [SerializeField] private string _details;
    [SerializeField] private string _largeArtwork;
    [SerializeField] private string _largeText;
    [SerializeField] private string _smallArtwork;
    [SerializeField] private string _smallText;
    [SerializeField] private bool _instance;

    private void Awake()
    {
        StateName = this._stateName;
        Details = this._details;
        LargeArtwork = this._largeArtwork;
        LargeText = this._largeText;
        Instance = this._instance;
        SmallArtwork = this._smallArtwork;
        SmallText = this._smallText;
    }
}
