using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KUIManager : MonoBehaviour
{
    [field: SerializeField] public Image _settingBG_Image { get; private set; }
    [field: SerializeField] public Image _keySettingBG_Image { get; private set; }
    [SerializeField] private Animator _player_animator;
    [field: SerializeField] public Slider _bgm_Slider { get; private set; }
    [field: SerializeField] public Slider _sfx_Slider { get; private set; }
    [SerializeField] private KKeyManager _keyManager;
    [SerializeField] private GameObject _reallyExitGameObject;
    [SerializeField] private Image _optionImage;
    [SerializeField] private GameObject _dialogue;
    private void Awake()
    {
        LoadSoundVolume();
        _keyManager = FindObjectOfType<KKeyManager>();
    }

    private void Update()
    {
        if (_dialogue.activeSelf)
        {
            _optionImage.raycastTarget = false;
        }
        else
        {
            _optionImage.raycastTarget = true;
        }
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SettingKey]))
        {
            if (_settingBG_Image.gameObject.activeSelf)
            {
                OnClick_SettingExit_Btn();
            }
            else
            {
                if (KGameManager.Instance._canInput)
                {
                    _player_animator.SetBool("isWalking", false);
                    OnClick_Setting_Btn();
                }
            }
        }
    }


    public void OnClick_Setting_Btn()
    {
        _settingBG_Image.gameObject.SetActive(true);
        KGameManager.Instance.GamePause();
    }

    public void OnClick_SettingExit_Btn()
    {
        _settingBG_Image.gameObject.SetActive(false);
        SaveSoundVolume();
        KGameManager.Instance.GameContinue();
    }
    public void OnClick_GameExit_Btn()
    {
        _reallyExitGameObject.gameObject.SetActive(true);
    }

    public void OnClick_KeySetting_Btn()
    {
        _keySettingBG_Image.gameObject.SetActive(true);
    }

    public void OnClick_KeySettingApply_Btn()
    {
        _keySettingBG_Image.gameObject.SetActive(false);
        _keyManager.SaveKeySetting();
    }

    private void SaveSoundVolume()
    {
        KDontDestroyOnLoadManager.Instance._settingData._bgmSound = _bgm_Slider.value;
        KDontDestroyOnLoadManager.Instance._settingData._sfxSound = _sfx_Slider.value;
        KDontDestroyOnLoadManager.Instance.SaveSettingData();
    }

    private void LoadSoundVolume()
    {
        KDontDestroyOnLoadManager.Instance.LoadSettingData();
        _bgm_Slider.value = KDontDestroyOnLoadManager.Instance._settingData._bgmSound;
        _sfx_Slider.value = KDontDestroyOnLoadManager.Instance._settingData._sfxSound;
        KSoundManager.Instance.MixerSet();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ReallyExitGameObjectOff()
    {
        _reallyExitGameObject.SetActive(false);
    }
}
