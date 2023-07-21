using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KUIManager : MonoBehaviour
{
    [field:SerializeField] public Image SettingBG_Image { get; private set; }
    [SerializeField] private Animator Player_animator;
    [field:SerializeField] public Slider BGM_Slider { get; private set; }
    [field: SerializeField] public Slider SFX_Slider { get; private set; }
    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[KKeyAction.SETTING_KEY]))
        {
            if(SettingBG_Image.gameObject.activeSelf)
            {
                OnClick_SettingExit_Btn();
            }
            else
            {
                Player_animator.SetBool("isWalking", false);
                OnClick_Setting_Btn();
            }
        }
    }

    public void OnClick_Setting_Btn()
    {
        SettingBG_Image.gameObject.SetActive(true);
        KGameManager.instance.GamePause();
    }
    public void OnClick_SettingExit_Btn()
    {
        SettingBG_Image.gameObject.SetActive(false);
        KGameManager.instance.GameContinue();
    }
    public void OnClick_GameExit_Btn()
    {
        Application.Quit();
    }
}
