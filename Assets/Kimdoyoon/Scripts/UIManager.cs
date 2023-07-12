using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image SettingBG_Image;

    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[KKeyAction.SETTING_KEY]))
        {
            OnClick_Setting_Btn();
        }
    }

    public void OnClick_Setting_Btn()
    {
        SettingBG_Image.gameObject.SetActive(true);
    }
    public void OnClick_SettingExit_Btn()
    {
        SettingBG_Image.gameObject.SetActive(false);
    }
    public void OnClick_GameExit_Btn()
    {
        Application.Quit();
    }
}
