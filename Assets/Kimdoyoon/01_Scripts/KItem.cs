using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KItem : MonoBehaviour
{
    [field:SerializeField] public KItemData data { get; private set; }
    public KDialogue[] dialogues_01;
    private void Start()
    {
        dialogues_01 = GetComponent<KInteractiveObject>().GetDialogue();
    }
}
