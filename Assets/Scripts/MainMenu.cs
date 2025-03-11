using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resMenu;
    public bool fullScreen = true;

    void Start()
    {
        fullScreen = Screen.fullScreen;
    }

    public void SetSettings(int Level)
    {
        QualitySettings.SetQualityLevel(Level, true);
    }

    public void SetRes()
    {

        if (resMenu.value == 1)
        {
            Screen.SetResolution(1280, 720, fullScreen);
        }

        else if (resMenu.value == 2)
        {
            Screen.SetResolution(1920, 1080, fullScreen);
        }

        else if (resMenu.value == 3)
        {
            Screen.SetResolution(2560, 1440, fullScreen);
        }

        else if (resMenu.value == 4)
        {
            Screen.SetResolution(3840, 2160, fullScreen);
        }

        else return;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullScreen = !fullScreen;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
