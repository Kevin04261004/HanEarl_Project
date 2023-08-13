using UnityEngine;
using UnityEngine.UI;

public enum EButtonState
{
    ChangeLine,
    GetItem,
    Exit_Btn,
    StartTimeLine_Act4_1_At_Roof,
    GoToMainScene,
}

public class KUIButoon : MonoBehaviour
{
    private KDialogueReader _kDialogueReader;
    private Button _btn;
    public int _num = 0;
    private G_StageManager _stageManager;
    [SerializeField] private KFadeManager _fadeManager;
    private void OnEnable()
    {
        _stageManager = FindObjectOfType<G_StageManager>();
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

    private void OnClicked_StartTimeLine_Act4_1_Roof()
    {
        _kDialogueReader.StopReading();
        KTimeLineManager.Instance.StartTimeLine10Routine();
    }

    private void OnClicked_GoToMainScene()
    {
        _stageManager.AfterSchool();
    }
    private void Onclick_Exit()
    {
        _kDialogueReader.StopReading();
    }
}