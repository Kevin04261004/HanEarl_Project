using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInteractiveType
{
    DisappearItem,
    CantDisappearItem,
    NPC,
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
    public void Interactive()
    {
        switch(_interactiveType)
        {
            case EInteractiveType.DisappearItem:
                JItem item = GetComponent<JItem>();
                item.Get();
                break;
            case EInteractiveType.CantDisappearItem:
                break;
            case EInteractiveType.NPC:
                _dialogueReader.SetDialogue(GetDialogue());
                break;
            default:
                break;

        }
    }
}