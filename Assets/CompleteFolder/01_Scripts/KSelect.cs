using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KSelect : MonoBehaviour
{
    public GameObject _creat; // Creat UI
    public TextMeshProUGUI[] _slot_TMP;
    public TextMeshProUGUI _newPlayerName;
    private bool[] savefile = new bool[3];
    private bool[] _hasSavefile = new bool[3];

    private void Awake()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (File.Exists(KDataManger.Instance._path + i.ToString()))
            {
                _hasSavefile[i] = true;
                KDataManger.Instance._nowSlot = i;
                KDataManger.Instance.LoadData();

                _slot_TMP[i].text = KDataManger.Instance._nowPlayer._name;
            }
            else
            {
                _slot_TMP[i].text = "비어있음";
            }
        }

        KDataManger.Instance.DataClear();
    }

    public void OnClick_Slot(int number)
    {
        KDataManger.Instance._nowSlot = number;
        if (_hasSavefile[number])
        {
            KDataManger.Instance.LoadData();
            GoGame();
        }
        else
        {
            Creat();
        }
    }

    public void Creat()
    {
        _creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!_hasSavefile[KDataManger.Instance._nowSlot])
        {
            if (_newPlayerName.text == "")
            {
                return;
            }
            KDataManger.Instance._nowPlayer._name = _newPlayerName.text;
            KDataManger.Instance.SaveData();
        }
        SceneManager.LoadScene("KGameScene");
    }
}