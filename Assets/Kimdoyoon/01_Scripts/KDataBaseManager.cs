using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDataBaseManager : MonoBehaviour
{
    public static KDataBaseManager Instance;
    [SerializeField] TextAsset _csv_File;
    [SerializeField] private KDialogue[] _dialogues;
    Dictionary<int, KDialogue> _dialogueDic = new Dictionary<int, KDialogue>();


    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            KCSVParser theParser = GetComponent<KCSVParser>();
            _dialogues = theParser.Parse(_csv_File.name);
            for(int i = 0; i< _dialogues.Length; i++)
            {
                _dialogueDic.Add(i + 1, _dialogues[i]);
            }
        }
    }

    public KDialogue[] GetDialogue(int startNum, int endNum)
    {
        List<KDialogue> dialogueList = new List<KDialogue>();

        for (int i = 0; i <= endNum - startNum; i++)
        {
            dialogueList.Add(_dialogueDic[startNum + i]);
        }
        return dialogueList.ToArray();
    }
}