using System;
using UnityEngine;

[Serializable]
public class Data : JData
{
    
}

public class DontDestroyOnLoadManager : MonoBehaviour
{
    public static DontDestroyOnLoadManager Instance;
    [SerializeField] private float _BgmSound_Float;
    [SerializeField] private float _SfxSound_Float;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            print(1);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);   
        }
        // 사운드 로드해서 초기화
        // 여기서 키 세팅들 초기화
    }
    
    
}
