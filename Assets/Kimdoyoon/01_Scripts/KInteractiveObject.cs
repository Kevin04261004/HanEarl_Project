using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveState
{
    Disappearitem,
    cantDisappearitem,
    NPC,
}
public class KInteractiveObject : MonoBehaviour
{
    [SerializeField] private InteractiveState thisState;
    [SerializeField] private KDialogueEvent dialogueEvent;
    [SerializeField] private KDialogueReader dialogueReader;

    public KDialogue[] GetDialogue()
    {
        dialogueReader = FindObjectOfType<KDialogueReader>();
        dialogueEvent.dialogues = KDataBaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        return dialogueEvent.dialogues;
    }
    public void Interactive()
    {
        switch(thisState)
        {
            case InteractiveState.Disappearitem:
                JItem item = GetComponent<JItem>();
                item.Get();
                break;
            case InteractiveState.cantDisappearitem:
                break;
            case InteractiveState.NPC:
                KDialogue[] temp = GetDialogue();
                dialogueReader.SetDialogue(temp);
                break;
            default:
                break;

        }
    }
}