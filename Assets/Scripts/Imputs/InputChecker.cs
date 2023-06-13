using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public Input input;
    

    [SerializeField] UiSmartPhone uiSmartPhone;
    [SerializeField] UiPause uiPause;

    void Awake()
    {
#if UNITY_ANDROID
        input = new InputPhone(uiSmartPhone, uiPause);
#elif UNITY_IOS
        input = new InputPhone(uiSmartPhone, uiPause);
#else
        input = new InputKeyboard();
#endif
    }

    void Update()
    {
        input.CheckInputsInUpdate();
    }
}
