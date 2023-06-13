using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int score {get; private set; }


    void Start()
    {
        DelegateController.GameStart += SetScoreZero;
        DelegateController.GameRestart += SetScoreZero;
        DelegateController.lineDestroing += IncreaseScore;
    }
    void SetScoreZero()
    {
        score = 0;
    }

    void IncreaseScore()
    {
        score++;
    }
}
