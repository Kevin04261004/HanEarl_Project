using System;
using UnityEngine;

[Serializable]
public class KSettingData : JData
{
    public float _bgmSound;
    public float _sfxSound;
    public KeyCode[] _keyData = new KeyCode[(int)EKeyAction.KeySize];
    public int _width;
    public int _height;
    public bool _isFullScreen;
}
public class KDontDestroyOnLoadManager : MonoBehaviour
{
    public static KDontDestroyOnLoadManager Instance;
    public KSettingData _settingData = new KSettingData();
    private void Start()
    {
        Application.targetFrameRate = 60;
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);   
        }

        LoadSettingData();
        Screen.SetResolution(_settingData._width, _settingData._height, _settingData._isFullScreen);
    }

    public void SaveSettingData()
    {
        JDataManager.instance.SaveData(_settingData);
    }

    public void LoadSettingData()
    {
        JDataManager.instance.Load(out _settingData);
    }
}