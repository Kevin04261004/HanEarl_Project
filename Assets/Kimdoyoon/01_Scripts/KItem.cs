using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KItem : MonoBehaviour
{
    public KDialogue[] _dialogues;
    private void Start()
    {
        _dialogues = GetComponent<KInteractiveObject>().GetDialogue();
    }
}
