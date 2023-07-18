using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KDialogueReader : MonoBehaviour
{
    private KDialogue[] dialogues;
    private bool isReading;
    private float time;
    [SerializeField] private float typetime = 0.02f;
    private int wordIndex;
    public int typeIndex = 0;
    private int contextsIndex = 0;
    [SerializeField] private bool isTyping = false;

    [Header("UI")]
    [SerializeField] private GameObject dialogue_GO;
    [SerializeField] private Image chararcterImage_Image;
    [SerializeField] private TextMeshProUGUI name_TMP;
    [SerializeField] private TextMeshProUGUI dialogue_TMP;
    [SerializeField] private GameObject Btns_GO;
    [SerializeField] private TextMeshProUGUI[] Options_TMP;

    private void Update()
    {
        if(isReading && Input.GetKeyDown(KKeySetting.key_Dictionary[KKeyAction.INTERACTION_KEY]))
        {
            if(isTyping)
            {
                isTyping = false;
                wordIndex = 0;
                if(dialogues[typeIndex].hasOption)
                {
                    TalkPanel_Change(dialogues[typeIndex].character_Name,null, dialogues[typeIndex].hasOption);
                }
                else
                {
                    TalkPanel_Change(dialogues[typeIndex].character_Name, dialogues[typeIndex].contexts[contextsIndex], dialogues[typeIndex].hasOption);
                }
            }
            else
            {
                if(typeIndex == dialogues.Length)
                {
                    isTyping = false;
                    isReading = false;
                    Talk_SetActive_Bool(false);
                    typeIndex = 0;
                }
                else
                {
                    if (dialogues[typeIndex].hasOption)
                    {
                        return;   
                    }
                    isTyping = true;
                    if (dialogues[typeIndex].contexts.Length > contextsIndex+1)
                    {
                        contextsIndex++;
                    }
                    else
                    {
                        if(typeIndex+1 == dialogues.Length)
                        {
                            isTyping = false;
                            isReading = false;
                            Talk_SetActive_Bool(false);
                            typeIndex = -1;
                        }
                        typeIndex++;
                        contextsIndex = 0;
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (isTyping)
        {
            if (dialogues[typeIndex].hasOption)
            {
                isTyping = false;
                TalkPanel_Change(dialogues[typeIndex].character_Name, null, dialogues[typeIndex].hasOption);
                return;
            }
            time += Time.deltaTime;
            if (time > typetime)
            {
                time = 0;
                wordIndex++;
               
                if (wordIndex > dialogues[typeIndex].contexts[contextsIndex].Length)
                {
                    isTyping = false;
                    wordIndex = 0;
                }
                else
                {
                    TalkPanel_Change_WithTyping(dialogues[typeIndex].character_Name, dialogues[typeIndex].contexts[contextsIndex], wordIndex);
                }
            }
        }
    }
    public void SetDialogue(KDialogue[] _forSetting)
    {
        dialogues = _forSetting;
        StartReading();
    }
    public void TalkPanel_Change(string _name, string _content = null, bool isOption = false)
    {
        Talk_SetActive_Bool(true);
        OptionBtn_SetActive_Bool(isOption);
        if (_content != null)
        {
            name_TMP.text = _name;
            dialogue_TMP.text = _content;
        }
    }
    public void TalkPanel_Change_WithTyping(string name, string content = null, int typingIndex = 0)
    {
        if (!dialogue_GO.activeSelf)
        {
            Talk_SetActive_Bool(true);
        }
        if (content == null)
        {
            //Talk_SetActive_Bool(false);
        }
        else
        {
            name_TMP.text = name;
            dialogue_TMP.text = content.Substring(0, typingIndex);
        }
    }
    public void Talk_SetActive_Bool(bool isTrue)
    {
        if (isTrue)
        {
            KGameManager.instance.canInput = false;
            dialogue_GO.SetActive(true);
        }
        else
        {
            KGameManager.instance.canInput = true;
            dialogue_GO.SetActive(false);
        }
    }
    public void OptionBtn_SetActive_Bool(bool isTrue)
    {
        if(isTrue)
        {
            Btns_GO.SetActive(true);
            for (int i = 0; i < dialogues[typeIndex].option_Contexts.Length; ++i)
            {
                Options_TMP[i].text = dialogues[typeIndex].option_Contexts[i];
                Options_TMP[i].transform.parent.GetComponent<KUIButoon>().num = int.Parse(dialogues[typeIndex].nextLine[i]);
                Options_TMP[i].transform.parent.GetComponent<KUIButoon>().AddListener(ButtonState.forChangeLine);
            }
        }
        else
        {
            Btns_GO.SetActive(false);
        }
    }
    public void StartReading()
    {
        StartCoroutine(read());
    }
    private IEnumerator read()
    {
        yield return null;
        isReading = true;
        isTyping = true;
    }
}
