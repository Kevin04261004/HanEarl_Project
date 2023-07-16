using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    public string name;
    public int stage = 0;
}

public class KDataManger : MonoBehaviour
{
    public static KDataManger instance;
    public string path;
    public int nowSlot;
    public PlayerData nowPlayer = new PlayerData();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/Ksave";
        print(path);
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path+ nowSlot.ToString(), data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
