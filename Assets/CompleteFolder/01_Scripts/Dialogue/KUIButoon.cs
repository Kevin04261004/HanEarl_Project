using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EButtonState
{
    ChangeLine,
    GetItem,
    Exit_Btn,
    StartTimeLine_Act4_1_At_Roof,
    GoToMainScene,
    HairDryDied,
    Rope,
    Rooftop_Fence
}

public class KUIButoon : MonoBehaviour
{
    private KDialogueReader _kDialogueReader;
    private Button _btn;
    public int _num = 0;
    private G_StageManager _stageManager;
    private G_EndingTimeLineStart _endingTimeLineStart;
    [SerializeField] private KFadeManager _fadeManager;
    public GameObject _from_GameObject;

    private void OnEnable()
    {
        _stageManager = FindObjectOfType<G_StageManager>();
        _endingTimeLineStart = FindObjectOfType<G_EndingTimeLineStart>();
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
            case EButtonState.GoToMainScene:
                _btn.onClick.AddListener(OnClicked_GoToMainScene);
                break;
            case EButtonState.HairDryDied:
                _btn.onClick.AddListener(HairDryTimeLineStart);
                _btn.onClick.AddListener(FromGameObjectSetActiveFalse);
                break;
            case EButtonState.Rope:
                _btn.onClick.AddListener(RopeTimeLineStart);
                _btn.onClick.AddListener(FromGameObjectSetActiveFalse);
                break;
            case EButtonState.Rooftop_Fence:
                _btn.onClick.AddListener(RooftopTimeLineStart);
                _btn.onClick.AddListener(FromGameObjectSetActiveFalse);
                break;
            default:
                break;
        }
        _btn.onClick.AddListener(OptionBtn_SetActiveFalse);
    }

    private void OptionBtn_SetActiveFalse()
    {
        _kDialogueReader.OptionBtn_SetActive_Bool(false);   
    }
    private void OnClicked_ChangeLineAndStartReading()
    {
        _kDialogueReader.typeIndex += _num;
        _kDialogueReader.StartReading();
    }

    private void FromGameObjectSetActiveFalse()
    {
        _from_GameObject.SetActive(false);
    }
    private void OnClicked_StartTimeLine_Act4_1_Roof()
    {
        _kDialogueReader.StopReading();
        G_InventorySystem.Instance.J_RemoveItem("���� ����"); // 이거 한글 깨지니까 제발 UTF-8쓰삼.
        KTimeLineManager.Instance.StartTimeLine10Routine();
        G_DifurcationManager.Instance.CallEnding("BadEndingA");
    }

    private void OnClicked_GoToMainScene()
    {
        _stageManager.AfterSchool();
    }
    private void Onclick_Exit()
    {
        _kDialogueReader.StopReading();
    }

    private void HairDryTimeLineStart()
    {
        _endingTimeLineStart.HairDry();
        _kDialogueReader.StopReading();
    }

    private void RopeTimeLineStart()
    {
        _endingTimeLineStart.Rope();
        _kDialogueReader.StopReading();
    }

    private void RooftopTimeLineStart()
    {
        _endingTimeLineStart.Rooftop();
        _kDialogueReader.StopReading();
    }
}