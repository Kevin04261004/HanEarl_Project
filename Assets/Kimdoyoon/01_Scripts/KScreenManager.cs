using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KScreenManager : MonoBehaviour
{
    private bool isFullScreen;

    private void Awake()
    {
        isFullScreen = false;
        Screen.SetResolution(Screen.width, Screen.height,false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[KKeyAction.SETFULLSCREEN_KEY]) && KGameManager.instance.canInput)
        {
            SetFullScreen();
        }
    }

    private void SetFullScreen()
    {
        if(!isFullScreen)
        {
            isFullScreen = true;
        }
        else
        {
            isFullScreen = false;
        }
        Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
    }
}
