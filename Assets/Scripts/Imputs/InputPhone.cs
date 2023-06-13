
public class InputPhone : Input
{
    public override void CheckInputsInUpdate() 
    {
        //do nothing
    }

    public InputPhone(UiSmartPhone uiSmartPhone, UiPause uiPause)
    {
        uiSmartPhone.LeftButton.onClick.AddListener(OnStepLeft);
        uiSmartPhone.RightButton.onClick.AddListener(OnStepRight);
        uiSmartPhone.DownButton.onClick.AddListener(OnStepDown);
        uiSmartPhone.FallButton.onClick.AddListener(OnFall);
        uiSmartPhone.LeftRotateButton.onClick.AddListener(OnRotateLeft);
        uiSmartPhone.RightRotateButton.onClick.AddListener(OnRotateRight);
        uiSmartPhone.PauseButton.onClick.AddListener(OnP);

        uiPause.SmartPhonesUnpause.onClick.AddListener(OnP);
    }
}
