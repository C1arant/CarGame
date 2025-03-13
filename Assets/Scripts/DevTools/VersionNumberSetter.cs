using UnityEngine;

public class VersionNumberSetter : MonoBehaviour
{
    private string versionNumber = "";
    public bool isPatch = false;
    public bool isDevelopment = false; // New boolean to mark as development version
    public static VersionNumberSetter instance;

    // Optional: Reference to TextMeshProUGUI if not on the same GameObject
    [SerializeField] private TMPro.TextMeshProUGUI versionText;

    void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Get the version number
        versionNumber = Application.version;

        // Handle patch versioning
        if (isPatch)
        {
            int lastIndexOf = versionNumber.LastIndexOf(".");
            if (lastIndexOf >= 0)
            {
                versionNumber = versionNumber.Substring(0, lastIndexOf);
            }
        }

        // Construct the version string with development prefix if applicable
        string displayText = "Version: ";
        if (isDevelopment)
        {
            displayText = "<color=red>[Development Build]</color> " + displayText;
        }
        displayText += versionNumber;

        // Try to get TextMeshProUGUI component
        TMPro.TextMeshProUGUI textComponent = versionText != null ? versionText : GetComponent<TMPro.TextMeshProUGUI>();

        // Update the text if found
        if (textComponent != null)
        {
            textComponent.text = displayText;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI not found. Either attach it to GameObject or assign it in the Inspector: " + gameObject.name);
        }
    }
}