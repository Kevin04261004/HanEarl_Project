using System;
using UnityEngine;

[Serializable]
public class KSettingData : JData
{
    public float _bgmSound;
    public float _sfxSound;
    public KeyCode[] _keyData = new KeyCode[(int)EKeyAction.KeySize];
}
public class KDontDestroyOnLoadManager : MonoBehaviour
{
    public static KDontDestroyOnLoadManager Instance;
    public KSettingData _settingData = new KSettingData();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);   
        }
        // 사운드 로드해서 초기화
        
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