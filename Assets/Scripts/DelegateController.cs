using UnityEngine;
using System;

public static class DelegateController
{
    public static Action GameStart;
    public static Action GameRestart;
    public static Action SwitchMusicInBackMenu;
    public static Action gameplayStopContinue;
    public static Action gameOver;
    public static Action rotateImpossible;
    public static Action FigureChanged;
    public static Action lineDestroing;
    public static Action <Vector2> blockDestroing;
    public static Action LeaderboardOpening;
    public static Action <string, int> PlayerAdded;
    public static Action <Ui> PlayerAddedOpenLeaderboard;
    public static Action TimeStop;
    public static Action TimeStartMove;
}
