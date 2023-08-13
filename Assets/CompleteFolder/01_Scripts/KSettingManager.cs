using TMPro;
using UnityEngine;

public class KSettingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _TMP;
    [SerializeField] private TextMeshProUGUI _skip_TMP;
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
                case "Alpha0":
                    str = "0";
                    break;
                case "Alpha1":
                    str = "1";
                    break;
                case "Alpha2":
                    str = "2";
                    break;
                case "Alpha3":
                    str = "3";
                    break;
                case "Alpha4":
                    str = "4";
                    break;
                case "Alpha5":
                    str = "5";
                    break;
                case "Alpha6":
                    str = "6";
                    break;
                case "Alpha7":
                    str = "7";
                    break;
                case "Alpha8":
                    str = "8";
                    break;
                case "Alpha9":
                    str = "9";
                    break;
                case "Delete":
                    str = "del";
                    break;
                case "KeypadPeriod":
                    str = ".";
                    break;
                case "KeypadDivide":
                    str = "/";
                    break;
                case "KeypadMultiply":
                    str = "*";
                    break;
                case "KeypadMinus":
                    str = "-";
                    break;
                case "KeypadPlus":
                    str = "+";
                    break;
                case "KeypadEnter":
                    str = "Enter";
                    break;
                case "KeypadEquals":
                    str = "=";
                    break;
                case "Insert":
                    str = "Ins";
                    break;
                case "Exclaim":
                    str = "Exc";
                    break;
                case "DoubleQuote":
                    str = "\"";
                    break;
                case "Ampersand":
                    str = "&";
                    break;
                case "Quote":
                    str = "\'";
                    break;
                case "Asterisk":
                    str = "*";
                    break;
                case "Plus":
                    str = "+";
                    break;
                case "Comma":
                    str = ",";
                    break;
                case "Minus":
                    str = "-";
                    break;
                case "Period":
                    str = ".";
                    break;
                case "Slash":
                    str = "/";
                    break;
                case "Colon":
                    str = ":";
                    break;
                case "SemiColon":
                    str = ";";
                    break;
                case "Less":
                    str = "<";
                    break;
                case "Equals":
                    str = "=";
                    break;
                case "Greater":
                    str = ">";
                    break;
                case "Question":
                    str = "?";
                    break;
                case "At":
                    str = "@";
                    break;
                case "LeftBracket":
                    str = "[";
                    break;
                case "Backslash":
                    str = "\\";
                    break;
                case "RightBracket":
                    str = "\"";
                    break;
                case "Underscore":
                    str = "_";
                    break;
                case "BackQuote":
                    str = "`";
                    break;
                default:
                    break;
            }

            if (KKeySetting.key_Dictionary[(EKeyAction)i] == KKeySetting.key_Dictionary[EKeyAction.SkipKey])
            {
                _skip_TMP.text = "Skip (" + str + ")";
            }
            _TMP[i].text = str;
        }
        
        
    }
    
}