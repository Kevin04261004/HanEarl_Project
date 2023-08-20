using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class KScreenManager : MonoBehaviour
{
    
    private bool _isFullScreen;
    [SerializeField] private TMP_Dropdown _resolution_Dropdown;
    [SerializeField] private TextMeshProUGUI _check_TMP;
    private List<Resolution> _resolutions = new List<Resolution>();
    [SerializeField] private int _resolutionNum;
    
    private void Start()
    {
        _isFullScreen = Screen.fullScreen;
        _check_TMP.text = _isFullScreen ? "V" : string.Empty;
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
            bool isSame = false;
            if ((float)t.height / t.width == (float)9/16)
            {
                foreach (var item in _resolutions)
                {
                    if (t.height == item.height && t.width == item.width)
                    {
                        isSame = true;
                        continue;
                    }
                }

                if (!isSame)
                {
                    _resolutions.Add(t);    
                }
                
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
        Screen.SetResolution(_resolutions[_resolutionNum].width, _resolutions[_resolutionNum].height, _isFullScreen);
        KDontDestroyOnLoadManager.Instance._settingData._width = Screen.width;
        KDontDestroyOnLoadManager.Instance._settingData._width = Screen.height;
        KDontDestroyOnLoadManager.Instance._settingData._isFullScreen = Screen.fullScreen;
        KDontDestroyOnLoadManager.Instance.SaveSettingData();
    }
}
