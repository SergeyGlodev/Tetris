using UnityEngine;
using System.Collections.Generic;

public class UiManager : MonoBehaviour
{
    Dictionary<Ui, UiMenus> uiMenus;
    
    [SerializeField] UiMenu uiMenu;
    [SerializeField] UiGameplay uiGameplay;
    [SerializeField] UiSettings uiSettings;
    [SerializeField] UiPause uiPause;
    [SerializeField] UiGameOver uiGameOver;
    [SerializeField] UiLeaderboard uiLeaderboard;
    [SerializeField] UiPlayerAdder uiPlayerAdder;

    [SerializeField] UiSmartPhone uiPhone;
    [SerializeField] GameObject uiKeyboardGameplay;

    public Ui CurrentUi {get; private set; }
    public Ui lastUi {get; private set; }
    

    public void StartNewGame()
    {
        OpenConcreateMenu(Ui.uiGameplay);

        if (lastUi == Ui.uiMenu)
        {
            DelegateController.GameStart?.Invoke();
        }
        else if(lastUi == Ui.uiGameOver || lastUi == Ui.uiPause)
        {
            DelegateController.GameRestart?.Invoke();
        }
    }

    public void OpenConcreateMenu(Ui targetUi)
    {
        ActivateUi(targetUi);
        
        lastUi = CurrentUi;
        CurrentUi = targetUi;
        
        uiMenus[CurrentUi].UiOn();
        uiMenus[lastUi].UiOff();
    }
    public void OpenAdditionalMenu(Ui targetUi)
    {
        ActivateAdditionalUi(targetUi);
    }
    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        uiSettings.SetResIndex();

        uiMenus = new Dictionary<Ui, UiMenus>()
        {
            {Ui.uiGameOver, uiGameOver},
            {Ui.uiGameplay, uiGameplay},
            {Ui.uiLeaderboard, uiLeaderboard},
            {Ui.uiMenu,uiMenu},
            {Ui.uiPause, uiPause},
            {Ui.uiPlayerAdder, uiPlayerAdder},
            {Ui.uiSettings, uiSettings},
        };

#if UNITY_ANDROID
        uiPhone.gameObject.SetActive(true);
        uiKeyboardGameplay.SetActive(false);
#elif UNITY_IOS
        uiPhone.gameObject.SetActive(true);
        uiKeyboardGameplay.SetActive(false);
#else
        uiPhone.gameObject.SetActive(false);
        uiKeyboardGameplay.SetActive(true);
#endif

        CurrentUi = Ui.uiMenu;
        ActivateUi(Ui.uiMenu);
        
        DelegateController.gameOver += GameOver;

        DelegateController.LeaderboardOpening += uiLeaderboard.ShowPlayers;
        DelegateController.PlayerAddedOpenLeaderboard += OpenConcreateMenu;

        DelegateController.GameStart += uiGameplay.UpdateScore;
        DelegateController.GameRestart += uiGameplay.UpdateScore;
        DelegateController.lineDestroing += uiGameplay.UpdateScore;
        DelegateController.GameStart += uiGameplay.TimeReset;
        DelegateController.GameRestart += uiGameplay.TimeReset;

        ManagersCache.instance.InputChecker.input.escape += EscapePressed;
        ManagersCache.instance.InputChecker.input.OnPPressed += PausePressed;
    }

    void Update()
    {
        if (CurrentUi == Ui.uiGameplay)
        {
            uiGameplay.UpdateGameplayTime();
            uiGameplay.SetGameplayTime();
            uiGameplay.UpdateScore();
        }
    }
    void PausePressed()
    {
        if (CurrentUi == Ui.uiGameplay)
        {
            OpenConcreateMenu(Ui.uiPause);
        }
        else if(CurrentUi == Ui.uiPause)
        {
            OpenConcreateMenu(Ui.uiGameplay);
        }
    }
    void EscapePressed()
    {
        if (CurrentUi == Ui.uiGameplay)
        {
            OpenConcreateMenu(Ui.uiPause);
        }
        else if(CurrentUi == Ui.uiPause)
        {
            OpenConcreateMenu(Ui.uiGameplay);
        }

        if (CurrentUi == Ui.uiSettings)
        {
            OpenConcreateMenu(lastUi);
        }
        else if (CurrentUi == Ui.uiMenu)
        {
            Quit();
        }
    }
    
    void GameOver()
    {
        OpenConcreateMenu(Ui.uiGameOver);
    }
    
    void ActivateUi(Ui uiToActivate)
    {
        uiMenu.gameObject.SetActive(false);
        uiGameplay.gameObject.SetActive(false);
        uiSettings.gameObject.SetActive(false);
        uiPause.gameObject.SetActive(false);
        uiGameOver.gameObject.SetActive(false);
        uiLeaderboard.gameObject.SetActive(false);
        uiPlayerAdder.gameObject.SetActive(false);

        switch (uiToActivate)
        {
            case Ui.uiGameOver: 
                uiGameOver.gameObject.SetActive(true);
                break;
            case Ui.uiGameplay: 
                uiGameplay.gameObject.SetActive(true);
                break;
            case Ui.uiLeaderboard: 
                uiLeaderboard.gameObject.SetActive(true);
                break;
            case Ui.uiMenu: 
                uiMenu.gameObject.SetActive(true);
                break;
            case Ui.uiPause: 
                uiPause.gameObject.SetActive(true);
                break;
            case Ui.uiPlayerAdder: 
                uiPlayerAdder.gameObject.SetActive(true);
                break;
            case Ui.uiSettings: 
                uiSettings.gameObject.SetActive(true);
                break;
        }
    }
    void ActivateAdditionalUi(Ui uiToActivate)
    {
        switch (uiToActivate)
        {
            case Ui.uiGameOver: 
                uiGameOver.gameObject.SetActive(true);
                break;
            case Ui.uiGameplay: 
                uiGameplay.gameObject.SetActive(true);
                break;
            case Ui.uiLeaderboard: 
                uiLeaderboard.gameObject.SetActive(true);
                break;
            case Ui.uiMenu: 
                uiMenu.gameObject.SetActive(true);
                break;
            case Ui.uiPause: 
                uiPause.gameObject.SetActive(true);
                break;
            case Ui.uiPlayerAdder: 
                uiPlayerAdder.gameObject.SetActive(true);
                break;
            case Ui.uiSettings: 
                uiSettings.gameObject.SetActive(true);
                break;
        }
    }
}
