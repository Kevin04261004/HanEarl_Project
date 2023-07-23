using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KItem : MonoBehaviour
{
    public KDialogue[] dialogues_01;
    public JItemInstance itemData;
    private void Start()
    {
        dialogues_01 = GetComponent<KInteractiveObject>().GetDialogue();
    }
}
