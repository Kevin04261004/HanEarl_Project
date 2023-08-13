using System;
using UnityEngine;

[Serializable]
public class KSettingData : JData
{
    public float _bgmSound;
    public float _SfxSound;
}

public class DontDestroyOnLoadManager : MonoBehaviour
{
    public static DontDestroyOnLoadManager Instance;
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
        // 여기서 키 세팅들 초기화
    }

    private void Start()
    {
        JDataManager.instance.SaveData(_settingData);   
    }
}
