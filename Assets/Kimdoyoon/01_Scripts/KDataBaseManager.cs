using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDataBaseManager : MonoBehaviour
{
    public static KDataBaseManager instance;
    [SerializeField] TextAsset csv_File;
    [SerializeField] private KDialogue[] dialogues;
    Dictionary<int, KDialogue> dialogueDic = new Dictionary<int, KDialogue>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            KCSVParser theParser = GetComponent<KCSVParser>();
            dialogues = theParser.Parse(csv_File.name);
            for(int i = 0; i< dialogues.Length; i++)
            {
                dialogueDic.Add(i+1, dialogues[i]);
            }
        }
    }

    public KDialogue[] GetDialogue(int _startNum, int _endNum)
    {
        List<KDialogue> dialogueList = new List<KDialogue>();

        for (int i = 0; i <= _endNum - _startNum; i++)
        {
            dialogueList.Add(dialogueDic[_startNum + i]);
        }
        return dialogueList.ToArray();
    }
}