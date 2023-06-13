using UnityEngine;
using UnityEngine.UI;

public class UiSmartPhone : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button downButton;
    [SerializeField] Button fallButton;
    [SerializeField] Button leftRotateButton;
    [SerializeField] Button rightRotateButton;
    [SerializeField] Button pauseButton;

    public Button LeftButton => leftButton;
    public Button RightButton => rightButton;
    public Button DownButton => downButton;
    public Button FallButton => fallButton;
    public Button LeftRotateButton => leftRotateButton;
    public Button RightRotateButton => rightRotateButton;
    public Button PauseButton => pauseButton;
}
