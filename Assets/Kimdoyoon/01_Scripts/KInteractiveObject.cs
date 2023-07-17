using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KInteractiveObject : MonoBehaviour
{
    [SerializeField] KDialogueEvent dialogueEvent;
    public KDialogue[] GetDialogue()
    {
        dialogueEvent.dialogues = KDataBaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        return dialogueEvent.dialogues;
    }
}
