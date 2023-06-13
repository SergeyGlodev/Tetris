using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalTimeScale
{
    public static void OnStart() 
    {
        DelegateController.TimeStop += SetTimeStop;
        DelegateController.TimeStartMove += SetTimeMove;
    }

    static void SetTimeMove()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    static void SetTimeStop()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
}
