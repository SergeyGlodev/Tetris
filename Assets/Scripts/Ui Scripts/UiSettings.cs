using UnityEngine;
using UnityEngine.UI;

public class UiSettings : UiMenus
{
    [SerializeField] Slider[] volumeSliders;
    [SerializeField] Toggle[] resolutionToggles;
    [SerializeField] int[] screenWidth;
    [SerializeField] Button backButton;
    [SerializeField] Toggle fullScreen;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject menuBackground;
    
    public GameObject Fade
    {
        get { return fade; }
        set { fade = value; }
    }
    public GameObject MenuBackground
    {
        get { return menuBackground; }
        set { menuBackground = value; }
    }

    int activeScreenResIndex;

    public override void UiOn()
    {
        base.UiOn();

        if (ManagersCache.instance.UiManager.lastUi == Ui.uiPause)
        {
            Fade.SetActive(true);
            MenuBackground.SetActive(false);
        }
        else if (ManagersCache.instance.UiManager.lastUi == Ui.uiMenu)
        {
            Fade.SetActive(false);
            MenuBackground.SetActive(true);
        }
    }

    public void SetResIndex()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("Screen Resolution Index");
    }

    void Start()
    {
        Fade = fade;
        MenuBackground = menuBackground;

        SetResIndex();

        bool isFullScreen = (PlayerPrefs.GetInt("Fullscreen") == 1) ? true : false;

        backButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(ManagersCache.instance.UiManager.lastUi));
        
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            int j = i;
            resolutionToggles[j].onValueChanged.AddListener ( (value) => SetScreenResolution(j)); 
        }
        fullScreen.onValueChanged.AddListener(SetFullScreen);

        volumeSliders[0].value = ManagersCache.instance.AudioManager.MasterVolumePercent;
        volumeSliders[0].onValueChanged.AddListener (SetMasterVolume);
        volumeSliders[1].value = ManagersCache.instance.AudioManager.MusicVolumePercent;
        volumeSliders[1].onValueChanged.AddListener (SetMusicVolume); 
        volumeSliders[2].value = ManagersCache.instance.AudioManager.SfxVolumePercent;
        volumeSliders[2].onValueChanged.AddListener (SetSfxVolume); 

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }
        SetFullScreen(isFullScreen);
        fullScreen.isOn = isFullScreen;
    }

    void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16f / 9f;
            Screen.SetResolution(screenWidth[i], (int)(screenWidth[i] / aspectRatio) + 1, false);

            PlayerPrefs.SetInt("Screen Resolution Index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    void SetFullScreen(bool isFullScreen)
    {
        for(int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullScreen;
        }

        if(isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("Fullscreen", ((isFullScreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    void SetMasterVolume(float value) 
    {
        ManagersCache.instance.AudioManager.SetVolume(value, AudioChannel.Master);
    }
    public void SetMusicVolume(float value) 
    {
        ManagersCache.instance.AudioManager.SetVolume(value, AudioChannel.Music);
    }
    public void SetSfxVolume(float value) 
    {
        ManagersCache.instance.AudioManager.SetVolume(value, AudioChannel.Sfx);
    }
}
