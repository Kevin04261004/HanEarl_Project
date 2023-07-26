using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EKeyAction
{
    UpKey,
    DownKey,
    LeftKey,
    RightKey,
    InteractionKey,
    SetFullScreenKey,
    SettingKey,
    InventoryKey,
    SightKey,
    KeySize,
}
public static class KKeySetting
{
    public static Dictionary<EKeyAction, KeyCode> key_Dictionary = new Dictionary<EKeyAction, KeyCode>();
}
public class KKeyManager : MonoBehaviour
{
    [SerializeField] private KSettingManager _kSettingManager;
    [SerializeField] int key = -1;
    private void Awake()
    {
        KKeySetting.key_Dictionary.Clear();
        KKeySetting.key_Dictionary.Add(EKeyAction.UpKey, KeyCode.UpArrow);
        KKeySetting.key_Dictionary.Add(EKeyAction.DownKey, KeyCode.DownArrow);
        KKeySetting.key_Dictionary.Add(EKeyAction.LeftKey, KeyCode.LeftArrow);
        KKeySetting.key_Dictionary.Add(EKeyAction.RightKey, KeyCode.RightArrow);
        KKeySetting.key_Dictionary.Add(EKeyAction.InteractionKey, KeyCode.F);
        KKeySetting.key_Dictionary.Add(EKeyAction.SetFullScreenKey, KeyCode.F5);
        KKeySetting.key_Dictionary.Add(EKeyAction.SettingKey, KeyCode.Escape);
        KKeySetting.key_Dictionary.Add(EKeyAction.InventoryKey, KeyCode.Tab);
        KKeySetting.key_Dictionary.Add(EKeyAction.SightKey, KeyCode.Space);
    }
    private void OnGUI()
    {
        var keyEvent = Event.current;
        if (!keyEvent.isKey)
        {
            return;
        }
        KKeySetting.key_Dictionary[(EKeyAction)key] = keyEvent.keyCode;
        _kSettingManager.Setting_KeyUI();
        key = -1;
    }
    public void ChangeKey(int num)
    {
        key = num;
    }
}
