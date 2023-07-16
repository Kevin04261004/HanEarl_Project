using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KSelect : MonoBehaviour
{
    public GameObject creat; // �г��� �Է� UI
    public TextMeshProUGUI[] slot_TMP;
    public TextMeshProUGUI newPlayerName;

    private bool[] savefile = new bool[3];
    private void Awake()
    {
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists(KDataManger.instance.path + i.ToString()))
            {
                savefile[i] = true;
                KDataManger.instance.nowSlot = i;
                KDataManger.instance.LoadData();

                slot_TMP[i].text = KDataManger.instance.nowPlayer.name;
            }
            else
            {
                slot_TMP[i].text = "�������";
            }
        }
        KDataManger.instance.DataClear();
    }
    public void OnClick_Slot(int number)
    {
        KDataManger.instance.nowSlot = number;
        if (savefile[number])
        {
            KDataManger.instance.LoadData();
            GoGame();
        }
        else
        {
            Creat();
        }
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }
    public void GoGame()
    {
        if (!savefile[KDataManger.instance.nowSlot])
        {
            if(newPlayerName.text == "")
            {
                return;
            }
            KDataManger.instance.nowPlayer.name = newPlayerName.text;
            KDataManger.instance.SaveData();
        }
        SceneManager.LoadScene("KGameScene");
    }
}
