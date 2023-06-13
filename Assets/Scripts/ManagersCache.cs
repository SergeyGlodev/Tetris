using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersCache : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] SpritesLibrary spritesLibrary;
    [SerializeField] MusicManager musicManager;
    [SerializeField] JSONSaving jSONSaving;
    [SerializeField] AudioManager audioManager;
    [SerializeField] InputChecker inputChecker;

    public UiManager UiManager => uiManager;
    public SpritesLibrary SpritesLibrary => spritesLibrary;
    public MusicManager MusicManager => musicManager;
    public JSONSaving JSONSaving => jSONSaving;
    public AudioManager AudioManager => audioManager;
    public InputChecker InputChecker => inputChecker;

    public static ManagersCache instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }
}
