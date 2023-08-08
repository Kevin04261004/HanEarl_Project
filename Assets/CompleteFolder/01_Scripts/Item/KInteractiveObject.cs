using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInteractiveType
{
    DisappearItem,
    CantDisappearItem,
    NPC,
    First_meet_C,
    First_meet_B,
    HairDry_item,
    Roop_item,
    RooftopFence,
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
                JItem item = GetComponent<JItem>();
                item.Get();
                _dialogueReader.SetDialogue(GetDialogue());
                break;
            case EInteractiveType.CantDisappearItem:
                break;
            case EInteractiveType.NPC:
                _dialogueReader.SetDialogue(GetDialogue());
                break;
            case EInteractiveType.First_meet_C:
                if (TryGetComponent(out KCManager kcManager))
                {
                    kcManager.FirstMeetInteractiveRoutine();
                }
                break;
            case EInteractiveType.First_meet_B:
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
            default:
                break;

        }

        G_DifurcationManager.Instance.AddInteractionObj(this.gameObject);
        Debug.Log(this.gameObject.name);
    }
}