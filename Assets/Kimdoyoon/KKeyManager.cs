using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KeyAction
{
    UP_KEY,
    DOWN_KEY,
    LEFT_KEY,
    RIGHT_KEY,
}
public static class KKeySetting
{
    public static Dictionary<KeyAction, KeyCode> key_Dictionary = new Dictionary<KeyAction, KeyCode>();
}
public class KKeyManager : MonoBehaviour
{
    private void Awake()
    {
        KKeySetting.key_Dictionary.Clear();
        KKeySetting.key_Dictionary.Add(KeyAction.UP_KEY, KeyCode.UpArrow);
        KKeySetting.key_Dictionary.Add(KeyAction.DOWN_KEY, KeyCode.DownArrow);
        KKeySetting.key_Dictionary.Add(KeyAction.LEFT_KEY, KeyCode.LeftArrow);
        KKeySetting.key_Dictionary.Add(KeyAction.RIGHT_KEY, KeyCode.RightArrow);

    }
}
