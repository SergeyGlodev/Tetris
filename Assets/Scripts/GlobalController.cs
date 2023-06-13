using UnityEngine;

public class GlobalController : MonoBehaviour
{
    [SerializeField] GameObject field ;
    [SerializeField] FieldController fieldController;

    void Awake()
    {
        DelegateController.GameStart += OnStartGame;
        DelegateController.GameRestart += OnGameRestart;
        DelegateController.gameOver += OnGameOver;

        GlobalTimeScale.OnStart();
    }

    void OnStartGame()
    {
        field.SetActive(true);
        fieldController.gameObject.SetActive(true);

        fieldController.StartGameplay();
    }
    void OnGameRestart()
    {
        field.SetActive(true);
        fieldController.gameObject.SetActive(true);

        fieldController.StartGameplay();
    }
    void OnGameOver()
    {
        field.SetActive(false);
        fieldController.gameObject.SetActive(false);
    }
}
