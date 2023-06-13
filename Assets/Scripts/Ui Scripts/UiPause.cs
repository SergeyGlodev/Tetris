using UnityEngine;
using UnityEngine.UI;

public class UiPause : UiMenus
{
    [SerializeField] Button pausePlayAgainButton;
    [SerializeField] Button pauseSettingsButton;
    [SerializeField] Button pauseMainMenuButton;
    [SerializeField] Button smartPhonesUnpause;

    public Button SmartPhonesUnpause => smartPhonesUnpause;

    public override void UiOn()
    {
        base.UiOn();

        ManagersCache.instance.UiManager.OpenAdditionalMenu(Ui.uiGameplay);
        
        DelegateController.TimeStop?.Invoke();
    }

    public override void UiOff()
    {
        base.UiOff();
        
        if (ManagersCache.instance.UiManager.CurrentUi != Ui.uiSettings)
        {
            DelegateController.TimeStartMove?.Invoke();
        }
    }

    private void Start() 
    {
#if UNITY_ANDROID
        smartPhonesUnpause.gameObject.SetActive(true);
#elif UNITY_IOS 
        smartPhonesUnpause.gameObject.SetActive(true);
#endif
        pausePlayAgainButton.onClick.AddListener(ManagersCache.instance.UiManager.StartNewGame);
        pauseSettingsButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiSettings));
        pauseMainMenuButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiMenu));
    }
}
