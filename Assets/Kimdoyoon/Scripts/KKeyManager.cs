using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KKeyAction
{
    UP_KEY,
    DOWN_KEY,
    LEFT_KEY,
    RIGHT_KEY,
    GETITEM_KEY,
}
public static class KKeySetting
{
    public static Dictionary<KKeyAction, KeyCode> key_Dictionary = new Dictionary<KKeyAction, KeyCode>();
}
public class KKeyManager : MonoBehaviour
{
    private void Awake()
    {
        KKeySetting.key_Dictionary.Clear();
        KKeySetting.key_Dictionary.Add(KKeyAction.UP_KEY, KeyCode.UpArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.DOWN_KEY, KeyCode.DownArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.LEFT_KEY, KeyCode.LeftArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.RIGHT_KEY, KeyCode.RightArrow);
        KKeySetting.key_Dictionary.Add(KKeyAction.GETITEM_KEY, KeyCode.F);
    }
}
