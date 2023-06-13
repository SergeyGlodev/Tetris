using UnityEngine;
using UnityEngine.UI;

public class UiMenu : UiMenus
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button leaderboardButton;

    public override void UiOn()
    {
        base.UiOn();

        if (ManagersCache.instance.AudioManager.CurrentAudioClip != ManagersCache.instance.MusicManager.MenuTheme)
        {
            DelegateController.SwitchMusicInBackMenu?.Invoke();
        }
        DelegateController.gameplayStopContinue?.Invoke();
    }

    void Start() 
    {
        playButton.onClick.AddListener(ManagersCache.instance.UiManager.StartNewGame);
        quitButton.onClick.AddListener(ManagersCache.instance.UiManager.Quit);
        optionsButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiSettings));
        leaderboardButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiLeaderboard));
    }
}
