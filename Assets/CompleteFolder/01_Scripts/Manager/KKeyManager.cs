using System.Collections.Generic;
using UnityEngine;
public enum EKeyAction
{
    UpKey = 0,
    DownKey,
    LeftKey,
    RightKey,
    InteractionKey,
    SetFullScreenKey,
    SettingKey,
    InventoryKey,
    SightKey,
    SkipKey,
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
        KDontDestroyOnLoadManager.Instance.LoadSettingData();
        if (KDontDestroyOnLoadManager.Instance._settingData._keyData[0] == null 
            || KDontDestroyOnLoadManager.Instance._settingData._keyData[0] == KeyCode.None)
        {
            First_KeySetting();
            SaveKeySetting();
        }
        else
        {
            LoadKeySetting();
        }
    }

    private void First_KeySetting()
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
        KKeySetting.key_Dictionary.Add(EKeyAction.SkipKey,KeyCode.P);
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
        SaveKeySetting();
        key = -1;
    }
    public void ChangeKey(int num)
    {
        key = num;
    }

    private void SaveKeySetting()
    {
        for (int i = 0; i < (int)EKeyAction.KeySize; i++)
        {
            print(KKeySetting.key_Dictionary[(EKeyAction)i]);
            KDontDestroyOnLoadManager.Instance._settingData._keyData[i] = KKeySetting.key_Dictionary[(EKeyAction)i];
            KDontDestroyOnLoadManager.Instance.SaveSettingData();
        }
    }
    private void LoadKeySetting()
    {
        for (int i = 0; i < (int)EKeyAction.KeySize; i++)
        {
            KDontDestroyOnLoadManager.Instance.LoadSettingData();
            KKeySetting.key_Dictionary[(EKeyAction)i] = KDontDestroyOnLoadManager.Instance._settingData._keyData[i];
        }
    }
}
