using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KKeyAction
{
    UP_KEY,
    DOWN_KEY,
    LEFT_KEY,
    RIGHT_KEY,
    INTERACTION_KEY,
    SETFULLSCREEN_KEY,
    SETTING_KEY,
    INVENTORY_KEY,
    KEY_SIZE,
}
public static class KKeySetting
{
    public static Dictionary<KKeyAction, KeyCode> key_Dictionary = new Dictionary<KKeyAction, KeyCode>();
}
public class KKeyManager : MonoBehaviour
{
    [SerializeField] private SettingManager settingManager;
    [SerializeField] int key = -1;
    private void Awake()
    {
        KKeySetting.key_Dictionary.Clear();
        KKeySetting.key_Dictionary.Add(KKeyAction.UP_KEY, KeyCode.UpArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.DOWN_KEY, KeyCode.DownArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.LEFT_KEY, KeyCode.LeftArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.RIGHT_KEY, KeyCode.RightArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.INTERACTION_KEY, KeyCode.F);
        KKeySetting.key_Dictionary.Add(KKeyAction.SETFULLSCREEN_KEY, KeyCode.F5);
        KKeySetting.key_Dictionary.Add(KKeyAction.SETTING_KEY, KeyCode.Escape);
        KKeySetting.key_Dictionary.Add(KKeyAction.INVENTORY_KEY, KeyCode.Tab);

    }
    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if(keyEvent.isKey)
        {
            KKeySetting.key_Dictionary[(KKeyAction)key] = keyEvent.keyCode;
            settingManager.Setting_KeyUI();
            key = -1;
        }
    }
    public void ChangeKey(int num)
    {
        key = num;
    }
}
