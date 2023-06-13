using UnityEngine;

public class InputKeyboard : Input
{
    public override void CheckInputsInUpdate() 
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            OnP();
        }
        if(UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape();
        }
        
        if (ManagersCache.instance.UiManager.CurrentUi == Ui.uiGameplay)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.A) || UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnStepLeft();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.D) || UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnStepRight();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.S) || UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnStepDown();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OnFall();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                OnRotateLeft();
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                OnRotateRight();
            }
        }
        
    }
}
