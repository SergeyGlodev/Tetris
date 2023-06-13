using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiGameOver :  UiMenus
{
    [SerializeField] Image fadePlane;
    [SerializeField] Button gameOverPlayAgainButton;
    [SerializeField] Button gameOverMainMenuButton;
    [SerializeField] Button gameOverSaveScoreButton;
    [SerializeField] Text gameOverScore;

    public Image FadePlane => fadePlane;


    public override void UiOn()
    {
        base.UiOn();
        
        FadePlane.DOFade(0.6f, 2f);
        OnGameOver();
    }

    public void OnGameOver()
    {
        gameOverScore.text = "Score: " + ScoreKeeper.score;
    }

    void Start()
    {
        gameOverPlayAgainButton.onClick.AddListener(ManagersCache.instance.UiManager.StartNewGame);
        gameOverMainMenuButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiMenu));
        gameOverSaveScoreButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiPlayerAdder));
    }
}
