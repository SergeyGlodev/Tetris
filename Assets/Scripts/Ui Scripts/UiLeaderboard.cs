using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UiLeaderboard : UiMenus
{
    [SerializeField] Button backToMainMenu;
    [SerializeField] TMP_Text [] players;
    [SerializeField] TMP_Text bestPlayersTitle;

    int showedNames;

    public override void UiOn()
    {
        base.UiOn();

        DelegateController.LeaderboardOpening?.Invoke();
    }
    
    public void ShowPlayers()
    {
        if(ManagersCache.instance.JSONSaving.Leaderboard.playersInLeaderboard.Count >= 5)
        {
            showedNames = 5;

            bestPlayersTitle.text = "Top " + showedNames + " players!";
        }
        else
        {
            showedNames = ManagersCache.instance.JSONSaving.Leaderboard.playersInLeaderboard.Count;
            
            if (showedNames > 1)
            {
                bestPlayersTitle.text = "Top " + showedNames + " players!";
            }
            else if(showedNames == 1)
            {
                bestPlayersTitle.text = "Best player!";
            }
            else
            {
                bestPlayersTitle.text = "Leaderboard are empty. Change it! :)";
            }
        }

        ManagersCache.instance.JSONSaving.Leaderboard.playersInLeaderboard = ManagersCache.instance.JSONSaving.Leaderboard.playersInLeaderboard.OrderByDescending((p => p.score)).ToList();

        for (int i = 0; i < showedNames; i++)
        {
            string playerName = ManagersCache.instance.JSONSaving.Leaderboard.playersInLeaderboard[i].name;
            int playerScore = ManagersCache.instance.JSONSaving.Leaderboard.playersInLeaderboard[i].score;
            int position = i + 1;
            players[i].text = position.ToString() + " " + playerName + ", score: " + playerScore.ToString();
            players[i].gameObject.SetActive(true);
        }
    }

    void Start()
    {
        backToMainMenu.onClick.AddListener(() => ManagersCache.instance.UiManager.OpenConcreateMenu(Ui.uiMenu));
    }
}
