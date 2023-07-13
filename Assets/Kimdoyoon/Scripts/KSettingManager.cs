using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class KSettingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] tMP;
    private void Start()
    {
        Setting_KeyUI();
    }
    public void Setting_KeyUI()
    {
        for (int i = 0; i < tMP.Length; i++)
        {
            tMP[i].text = KKeySetting.key_Dictionary[(KKeyAction)i].ToString();
            if (tMP[i].text == "Return")
            {
                tMP[i].text = "Enter";
            }
            if (tMP[i].text == "Escape")
            {
                tMP[i].text = "Esc";
            }
        }
    }
}
