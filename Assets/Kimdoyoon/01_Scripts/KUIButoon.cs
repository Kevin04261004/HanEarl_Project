using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public enum EButtonState
{
    ChangeLine,
    GetItem,
}

public class KUIButoon : MonoBehaviour
{
    private KDialogueReader _kDialogueReader;
    private Button _btn;
    public int _num = 0;
    private void OnEnable()
    {
        _kDialogueReader = FindObjectOfType<KDialogueReader>();
        _btn = transform.GetComponent<Button>();
    }
    public void AddListener(EButtonState state)
    {
        _btn.onClick.RemoveAllListeners();
        switch (state)
        {
            case EButtonState.ChangeLine:
                _btn.onClick.AddListener(OnClicked);
                break;
            case EButtonState.GetItem:
                break;
            default:
                break;
        }
    }
    private void OnClicked()
    {
        _kDialogueReader.typeIndex = _num - 1;
        _kDialogueReader.OptionBtn_SetActive_Bool(false);
        _kDialogueReader.StartReading();
    }
}
