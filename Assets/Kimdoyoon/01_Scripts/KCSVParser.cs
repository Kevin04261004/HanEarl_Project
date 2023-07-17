using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KCSVParser : MonoBehaviour
{
    public List<KDialogue> dialogueList;
    public KDialogue[] Parse(string _CSVFileName)
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        if(!csvData)
        {
            Debug.Log("TextAsset의 파일을 CSVManager에서 파일을 추가해주세요.");
            return null;
        }
        dialogueList = new List<KDialogue>();
        string[] data = csvData.text.Split('\n');
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(',');
            KDialogue tempDialogue = new KDialogue();
            List<string> contextList = new List<string>();

            tempDialogue.character_Name = row[1];
            if (!(row[2] == ""))
            {
                tempDialogue.characterImage_Name = row[2];
            }
            if (!(row[3] == ""))
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
                contextList.Add(row[4]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while (row[0].ToString() == "");
            switch (tempDialogue.hasOption)
            {
                case true:
                    tempDialogue.option_Contexts = contextList.ToArray();
                    break;
                case false:
                    tempDialogue.contexts = contextList.ToArray();
                    break;
            }
            dialogueList.Add(tempDialogue);
        }
        return dialogueList.ToArray();
    }
}