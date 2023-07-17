using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image SettingBG_Image;
    [SerializeField] private Animator Player_animator;

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
