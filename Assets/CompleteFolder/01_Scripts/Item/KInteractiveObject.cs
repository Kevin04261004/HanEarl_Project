using UnityEngine;

public enum EInteractiveType
{
    DisappearItem,
    CantDisappearItem,
    NPC,
    C_Act3,
    B_Act6,
    KAfterDialogue_Changed_And_GetItem,
    KAfterDialogue_Changed,
    Roof_Key_Event,
    Toilet_Key_Event,
    Mirror_Event,
    Roof_Table_Event,
    SafeBox
}

public class KInteractiveObject : MonoBehaviour
{
    [SerializeField] private EInteractiveType _interactiveType;
    [SerializeField] private KDialogueEvent _dialogueEvent;
    [SerializeField] private KDialogueReader _dialogueReader;
    [SerializeField] private KDialogueEvent _secondDialogueEvent;
    [SerializeField] private short _interactiveCount;
    [SerializeField] private bool normalEndingChange = false;
    private void Awake()
    {
        _dialogueReader = FindAnyObjectByType<KDialogueReader>();
        Debug.Assert(_dialogueReader != null,"_dialogueReader를 찾지 못했습니다.");
    }
    
    private KDialogue[] GetDialogue()
    {
        if (normalEndingChange && G_DifurcationManager.Instance.NormalChangeEndingCheck())
        {
            _dialogueEvent.dialogues = KDataBaseManager.Instance.GetDialogue((int)_dialogueEvent.N_line.x, (int)_dialogueEvent.N_line.y);
            return _dialogueEvent.dialogues;
        }

        _dialogueEvent.dialogues = KDataBaseManager.Instance.GetDialogue((int)_dialogueEvent.line.x, (int)_dialogueEvent.line.y);
        return _dialogueEvent.dialogues;
    }

    private KDialogue[] GetSecondDialogue()
    {
        _dialogueEvent.dialogues = KDataBaseManager.Instance.GetDialogue((int)_secondDialogueEvent.line.x, (int)_secondDialogueEvent.line.y);
        return _dialogueEvent.dialogues;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Interactive()
    {
        switch(_interactiveType)
        {
            case EInteractiveType.DisappearItem:
                //if (TryGetComponent(out JItem item))
                //{
                //    item.Get();
                //}
                JItem[] item = GetComponents<JItem>();
                foreach(JItem item4 in item)
                {
                    item4.Get();
                }
                
                _dialogueReader.SetDialogue(GetDialogue(), gameObject);
                gameObject.SetActive(false);
                break;
            case EInteractiveType.CantDisappearItem:
                _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                break;
            case EInteractiveType.NPC:
                if (_interactiveCount < 1)
                {
                    _dialogueReader.SetDialogue(GetDialogue(),gameObject);   
                }
                else 
                {
                    if (_secondDialogueEvent.line.x < 1)
                    {
                        _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                    }
                    else
                    {
                        _dialogueReader.SetDialogue(GetSecondDialogue(),gameObject);   
                    }
                }
                break;
            case EInteractiveType.C_Act3:
                if (TryGetComponent(out KCManager kcManager))
                {
                    kcManager.FirstMeetInteractiveRoutine();
                }
                if (TryGetComponent(out KAfterDialogue d10))
                {
                    d10.Used();
                }
                break;
            case EInteractiveType.B_Act6:
                if (TryGetComponent(out KBManager kbManager))
                {
                    kbManager.FirstMeetInteractiveRoutine();
                }
                if (TryGetComponent(out KAfterDialogue d0))
                {
                    d0.Used();
                }
                break;
            case EInteractiveType.KAfterDialogue_Changed_And_GetItem:
                //if (TryGetComponent(out JItem item2))
                //{
                //    item2.Get();
                //}
                JItem[] item2 = GetComponents<JItem>();
                foreach(JItem item3 in item2)
                {
                    item3.Get();
                }
                if (TryGetComponent(out KAfterDialogue d))
                {
                    d.Used();
                }
                _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                break;
            case EInteractiveType.KAfterDialogue_Changed:
                if (TryGetComponent(out KAfterDialogue d2))
                {
                    d2.Used();
                }
                _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                break;
            case EInteractiveType.Roof_Key_Event:
                if (G_InventorySystem.Instance.HasItemF("옥상 열쇠"))
                {
                    _dialogueReader.SetDialogue(GetSecondDialogue(),gameObject);
                    gameObject.SetActive(false);
                }
                else
                {
                    _dialogueReader.SetDialogue(GetDialogue(),gameObject);   
                }
                break;
            case EInteractiveType.Toilet_Key_Event:
                if (G_InventorySystem.Instance.HasItemF("화장실 열쇠"))
                {
                    _dialogueReader.SetDialogue(GetSecondDialogue(),gameObject);
                    gameObject.SetActive(false);
                }
                else
                {
                    _dialogueReader.SetDialogue(GetDialogue(),gameObject);   
                }
                break;
            case EInteractiveType.Roof_Table_Event:
                if (TryGetComponent(out KAfterDialogue d3))
                {
                    d3.Used();
                }
                if (TryGetComponent(out G_RooftopTable table))
                {
                    table.InteractiveRoutine();
                }
                break;
            case EInteractiveType.SafeBox:
                JSafeBox.Instance.SetSafeBoxActive();
                if (TryGetComponent(out KAfterDialogue d1))
                {
                    JSafeBox.Instance.dia = d1;
                }
                break;
            default:
                break;
        }

        _interactiveCount++;
        G_DifurcationManager.Instance.AddInteractionObj(this.gameObject);
    }
}