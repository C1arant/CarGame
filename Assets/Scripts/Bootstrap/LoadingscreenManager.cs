using UnityEngine;

public class LoadingscreenManager : MonoBehaviour
{
    public static LoadingscreenManager Instance { get; private set; }
    [SerializeField] private GameObject _loadingScreenObject;
    [SerializeField] private GameObject _cameraObject;
    private static bool _isOn;

    private void Awake()
    {
        _isOn = true;
    }

    private void OnEnable()
    {
        if (Instance != null) DestroyImmediate(this.gameObject);
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Play()
    {
        _isOn = true;
        _loadingScreenObject.SetActive(true);
        _cameraObject.SetActive(true);
    }

    public void Stop()
    {
        _loadingScreenObject.SetActive(false);
        _cameraObject.SetActive(false);
        _isOn = false;
    }

    public static bool Status()
    {
        return _isOn;
    }
}
