using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip menuTheme;
    [SerializeField] AudioClip gameTheme;
    [SerializeField] float delayBetweenThemes = 1f;
    
    public AudioClip MenuTheme => menuTheme;
    public AudioClip GameTheme => gameTheme;

    private void Start() 
    {
        DelegateController.GameStart += OnMenuGameplaySwitch;
        DelegateController.SwitchMusicInBackMenu += OnMenuGameplaySwitch;
        PlayMusic();
    }
    
    void OnMenuGameplaySwitch()
    {
        if (ManagersCache.instance.UiManager.CurrentUi != ManagersCache.instance.UiManager.lastUi)
        {
            PlayMusic();
        }
    }
    
    void PlayMusic()
    {
        AudioClip clipToPlay = null;

        if(ManagersCache.instance.UiManager.CurrentUi == Ui.uiMenu)
        {
            clipToPlay = menuTheme;
        }
        else if(ManagersCache.instance.UiManager.CurrentUi == Ui.uiGameplay)
        {
            clipToPlay = gameTheme;
        }


        if(clipToPlay != null)
        {
            ManagersCache.instance.AudioManager.PlayMusic(clipToPlay, delayBetweenThemes);
        }
    }
}
