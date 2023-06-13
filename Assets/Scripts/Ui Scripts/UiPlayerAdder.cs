using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UiPlayerAdder : UiMenus
{
    [SerializeField] InputField inputField;
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text invalidName;

    Text userNameInputText;
    string playerName;

    public string PlayerName => playerName;

    public bool animationPlaying;

    void Start() 
    {
        userNameInputText = GetComponent<Text>();

        inputField.onEndEdit.AddListener(InputPlayerName);
        backButton.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiGameOver));
        backButton.onClick.AddListener(NulledField);
    }

    void InputPlayerName(string name)
    {
        if (name != null && UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            if (name.Contains(" "))
            {
                InvalidName();
                return;
            }

            playerName = name;
            Debug.Log(playerName);

            DelegateController.PlayerAdded?.Invoke(name, ScoreKeeper.score);
            DelegateController.PlayerAddedOpenLeaderboard?.Invoke(Ui.uiLeaderboard);

            NulledField();
        }
    }
    void InvalidName()
    {
        if (!animationPlaying)
        {
            animationPlaying = true;
            invalidName.DOFade(1f, 0.2f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() => {animationPlaying = false;});
        } 
    }
    void NulledField()
    {
        inputField.text = null;
    }
}
