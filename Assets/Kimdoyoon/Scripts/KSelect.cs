using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class KSelect : MonoBehaviour
{
    public GameObject creat; // 닉네임 입력 UI
    public TextMeshProUGUI[] slot_TMP;
    public TextMeshProUGUI newPlayerName;
    public KDataMaanger dataManager;
    bool[] savefile;
    private void Awake()
    {
        dataManager = FindAnyObjectByType<KDataMaanger>();
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists(dataManager.path + "${i}"))
            {
                savefile[i] = true;
                dataManager.nowSlot = i;
                //dataManager
            }
        }
    }
}
