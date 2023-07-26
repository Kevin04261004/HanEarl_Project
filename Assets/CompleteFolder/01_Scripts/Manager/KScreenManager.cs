using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KScreenManager : MonoBehaviour
{
    
    private bool _isFullScreen;
    [SerializeField] private TMP_Dropdown _resolution_Dropdown;
    [SerializeField] private TextMeshProUGUI _check_TMP;
    private List<Resolution> _resolutions = new List<Resolution>();
    [SerializeField] private int _resolutionNum;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        _isFullScreen = false;
        SetFullScreen();
    }

    private void Start()
    {
        InitUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SetFullScreenKey]))
        {
            SetFullScreen();
        }
    }

    private void InitUI()
    {
        foreach (var t in UnityEngine.Device.Screen.resolutions)
        {
            if ((float)t.height / t.width == (float)9/16)
            {
                _resolutions.Add(t);
            }
        }
        _resolution_Dropdown.options.Clear();
        int optionNum = 0;
        foreach (var item in _resolutions)
        {
            var option = new TMP_Dropdown.OptionData();
            option.text = item.width + "x" + item.height;
            _resolution_Dropdown.options.Add(option);

            if (item.width == UnityEngine.Device.Screen.width
                && item.height == UnityEngine.Device.Screen.height)
            {
                _resolutionNum = optionNum;
            }

            optionNum++;
        }
        _resolution_Dropdown.RefreshShownValue();
    }
    
    public void SetFullScreen()
    {
        _isFullScreen = !_isFullScreen;
        _check_TMP.text = _isFullScreen ? "V" : string.Empty;
        Screen.SetResolution(Screen.width, Screen.height, _isFullScreen);
    }

    public void DropboxOptionChange(int x)
    {
        _resolutionNum = x;
    }

    public void OnClick_ScreenResolution_Btn()
    {
        UnityEngine.Device.Screen.SetResolution(_resolutions[_resolutionNum].width, _resolutions[_resolutionNum].height, _isFullScreen);
    }
}
