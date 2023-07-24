using UnityEngine;
using UnityEngine.UI;

public class KUIManager : MonoBehaviour
{
    [field:SerializeField] public Image _settingBG_Image { get; private set; }
    [SerializeField] private Animator _player_animator;
    [field:SerializeField] public Slider _bgm_Slider { get; private set; }
    [field: SerializeField] public Slider _sfx_Slider { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SettingKey]))
        {
            if(_settingBG_Image.gameObject.activeSelf)
            {
                OnClick_SettingExit_Btn();
            }
            else
            {
                _player_animator.SetBool("isWalking", false);
                OnClick_Setting_Btn();
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
        KGameManager.Instance.GameContinue();
    }
    public void OnClick_GameExit_Btn()
    {
        Application.Quit();
    }
}
