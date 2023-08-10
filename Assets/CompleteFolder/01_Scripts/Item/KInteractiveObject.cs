using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInteractiveType
{
    DisappearItem,
    CantDisappearItem,
    NPC,
    C_Act3,
    B_Act6,
    HairDry_item,
    Roop_item,
    RooftopFence,
    FairyTaleBook,
    
}

public class KInteractiveObject : MonoBehaviour
{
    [SerializeField] private EInteractiveType _interactiveType;
    [SerializeField] private KDialogueEvent _dialogueEvent;
    [SerializeField] private KDialogueReader _dialogueReader;

    private void Awake()
    {
        _dialogueReader = FindObjectOfType<KDialogueReader>();
        Debug.Assert(_dialogueReader != null,"_dialogueReader를 찾지 못했습니다.");
    }
    
    public KDialogue[] GetDialogue()
    {
        _dialogueEvent.dialogues = KDataBaseManager.Instance.GetDialogue((int)_dialogueEvent.line.x, (int)_dialogueEvent.line.y);
        return _dialogueEvent.dialogues;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Interactive()
    {
        switch(_interactiveType)
        {
            case EInteractiveType.DisappearItem:
                if (TryGetComponent(out JItem item))
                {
                    item.Get();
                }
                _dialogueReader.SetDialogue(GetDialogue(), gameObject);
                break;
            case EInteractiveType.CantDisappearItem:
                _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                break;
            case EInteractiveType.NPC:
                _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                break;
            case EInteractiveType.C_Act3:
                if (TryGetComponent(out KCManager kcManager))
                {
                    kcManager.FirstMeetInteractiveRoutine();
                }
                break;
            case EInteractiveType.B_Act6:
                if (TryGetComponent(out KBManager kbManager))
                {
                    kbManager.FirstMeetInteractiveRoutine();
                }
                break;
            case EInteractiveType.HairDry_item:
                if (TryGetComponent(out KHairDryItem hairdry))
                {
                    hairdry.InteractiveRoutine();
                }
                break;
            case EInteractiveType.Roop_item:
                if (TryGetComponent(out KRoop roop))
                {
                    roop.InteractiveRoutine();
                }
                break;
            case EInteractiveType.RooftopFence:
                if (TryGetComponent(out KRooftopFence rooftopFence))
                {
                    rooftopFence.InteractiveRoutine();
                }
                break;
            case EInteractiveType.FairyTaleBook:
                if (TryGetComponent(out JItem fairyTaleBook))
                {
                    fairyTaleBook.Get();
                }
                if (TryGetComponent(out KFairTaleBook book))
                {
                    book.Used();
                }
                _dialogueReader.SetDialogue(GetDialogue(),gameObject);
                break;
            default:
                break;

        }

        G_DifurcationManager.Instance.AddInteractionObj(this.gameObject);
    }
}