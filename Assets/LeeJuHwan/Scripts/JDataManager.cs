using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JDataManager : MonoBehaviour
{
    public static JDataManager instance;

    string path;
    string fileName = "PlayerData";
    public JPlayerData playerData = new JPlayerData();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        path = $"{Application.dataPath}/LeeJuHwan/JsonData/";
    }

    private void Start()
    {
        //path = "LeeJuHwan/JsonData/PlayerData";
        Debug.Log(path);
    }

    public void SaveData(JPlayerData wantdata)
    {
        string data = JsonUtility.ToJson(wantdata);
        File.WriteAllText(path + fileName, data);
    }

    public void LoadData()
    {
        Debug.Log(path);
        if (!File.Exists(path + fileName))
        {
            Debug.Log("파일이 존재하지 않음!");
            return;
        }
        string data = File.ReadAllText(path + fileName);
        Debug.Log(data);
        playerData = JsonUtility.FromJson<JPlayerData>(data);
    }
}
