using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCSVParser : MonoBehaviour
{
    public List<KDialogue> _dialogueList;
    public KDialogue[] Parse(string CSVFileName)
    {
        TextAsset csvData = Resources.Load<TextAsset>(CSVFileName);
        if(!csvData)
        {
            Debug.Log("TextAsset의 파일을 CSVManager에서 파일을 추가해주세요.");
            return null;
        }
        _dialogueList = new List<KDialogue>();
        string[] data = csvData.text.Split('\n');
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(',');
            KDialogue tempDialogue = new KDialogue();
            List<string> contextList = new List<string>();
            List<string> nextLineList = new List<string>();
            if (row[1] == "플레이어" && !string.IsNullOrEmpty(JDataManager.instance.playerData.name))
            {
                row[1] = row[1].Replace("플레이어", JDataManager.instance.playerData.name);
            }

            if (int.TryParse(row[0],out int a))
            {
                tempDialogue.index = a;
            }
            tempDialogue.character_Name = row[1];
            if (row[2] != string.Empty)
            {
                tempDialogue.characterImage_Name = row[2];
            }
            if (row[3] != string.Empty)
            {
                if (row[3] == "no")
                {
                    tempDialogue.hasOption = false;
                }
                else if (row[3] == "yes")
                {
                    tempDialogue.hasOption = true;
                }
            }
            do
            {
                if (!string.IsNullOrEmpty(JDataManager.instance.playerData.name))
                {
                    row[4] = row[4].Replace("플레이어", JDataManager.instance.playerData.name);   
                }
                row[4] = row[4].Replace("'", ",");
                contextList.Add(row[4]);
                if (row[5] != string.Empty)
                {
                    nextLineList.Add(row[5]);
                }
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while (row[0] == string.Empty);
            switch (tempDialogue.hasOption)
            {
                case true:
                    tempDialogue.option_Contexts = contextList.ToArray();
                    tempDialogue.nextLine = nextLineList.ToArray(); 
                    break;
                case false:
                    tempDialogue.contexts = contextList.ToArray();
                    tempDialogue.nextLine = nextLineList.ToArray(); 
                    break;
            }
            _dialogueList.Add(tempDialogue);
        }
        return _dialogueList.ToArray();
    }
}