using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KScreenManager : MonoBehaviour
{
    private bool _isFullScreen;

    private void Awake()
    {
        _isFullScreen = false;
        Screen.SetResolution(Screen.width, Screen.height,_isFullScreen);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SetFullScreenKey]) && KGameManager.Instance._canInput)
        {
            SetFullScreen();
        }
    }

    private void SetFullScreen()
    {
        if(!_isFullScreen)
        {
            _isFullScreen = true;
        }
        else
        {
            _isFullScreen = false;
        }
        Screen.SetResolution(Screen.width, Screen.height, _isFullScreen);
    }
}
