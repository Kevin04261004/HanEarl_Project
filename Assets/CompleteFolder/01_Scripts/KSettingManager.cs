using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class KSettingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _TMP;
    private void Start()
    {
        Setting_KeyUI();
    }
    public void Setting_KeyUI()
    {
        for (int i = 0; i < _TMP.Length; i++)
        {
            string str = KKeySetting.key_Dictionary[(EKeyAction)i].ToString();
            switch (str)
            {
                case "Return":
                    str = "Enter";
                    break;
                case "Escape":
                    str = "Esc";
                    break;
                default:
                    break;
            }
            _TMP[i].text = str;
        }
    }
}