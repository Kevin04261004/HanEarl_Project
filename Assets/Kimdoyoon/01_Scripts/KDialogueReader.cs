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
    private int typeIndex = 0;
    private int contextsIndex = 0;
    [SerializeField] private bool isTyping = false;

    [Header("UI")]
    [SerializeField] private GameObject dialogue_GO;
    [SerializeField] private Image chararcterImage_Image;
    [SerializeField] private TextMeshProUGUI name_TMP;
    [SerializeField] private TextMeshProUGUI dialogue_TMP;
    [SerializeField] private GameObject Btns_GO;
    [SerializeField] private TextMeshProUGUI Option_01_TMP;
    [SerializeField] private TextMeshProUGUI Option_02_TMP;
    [SerializeField] private TextMeshProUGUI Option_03_TMP;

    private void Update()
    {
        if(isReading && Input.GetKeyDown(KeyCode.Return))
        {
            if(isTyping)
            {
                isTyping = false;
                wordIndex = 0;
                TalkPanel_Change(dialogues[typeIndex].character_Name, dialogues[typeIndex].contexts[contextsIndex]);
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
                            typeIndex = 0;
                        }
                        typeIndex++;
                        contextsIndex = 0;
                    }
                }
            }
        }
    }
    public void SetDialogue(KDialogue[] _forSetting)
    {
        dialogues = _forSetting;
    }
    public void TalkPanel_Change(string _name, string _content = null)
    {
        Talk_SetActive_Bool(true);
        if (_content == null)
        {
            
        }
        else
        {
            name_TMP.text = _name;
            dialogue_TMP.text = _content;
        }
    }
    public void Talk_SetActive_Bool(bool isTrue)
    {
        if (isTrue && !dialogue_GO.activeSelf)
        {
            dialogue_GO.SetActive(true);
        }
        else
        {
            dialogue_GO.SetActive(false);
        }
    }
}
