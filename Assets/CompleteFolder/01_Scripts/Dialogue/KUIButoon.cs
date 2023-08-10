using System;
using UnityEngine;
using UnityEngine.UI;

public enum EButtonState
{
    ChangeLine,
    GetItem,
    Exit_Btn,
    StartTimeLine_Act4_1_At_Roof,
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
                _btn.onClick.AddListener(OnClicked_ChangeLineAndStartReading);
                break;
            case EButtonState.GetItem:
                
                break;
            case EButtonState.Exit_Btn:
                _btn.onClick.AddListener(Onclick_Exit);
                break;
            case EButtonState.StartTimeLine_Act4_1_At_Roof:
                _btn.onClick.AddListener(OnClicked_StartTimeLine_Act4_1_Roof);
                break;
            default:
                break;
        }
    }
    private void OnClicked_ChangeLineAndStartReading()
    {
        _kDialogueReader.typeIndex += _num;
        _kDialogueReader.OptionBtn_SetActive_Bool(false);
        _kDialogueReader.StartReading();
    }

    private void OnClicked_StartTimeLine_Act4_1_Roof()
    {
        _kDialogueReader.StopReading();
        KTimeLineManager.Instance.StartTimeLine("10");
    }

    private void Onclick_Exit()
    {
        _kDialogueReader.StopReading();
    }
}