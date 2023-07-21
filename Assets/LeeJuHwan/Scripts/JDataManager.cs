using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class JDataManager : MonoBehaviour
{
    public static JDataManager instance;

    string path;
    public JPlayerData playerData;
    public JItemData itemData;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        path = $"{Application.dataPath}/LeeJuHwan/JsonData";
        Load(out playerData);
        Load(out itemData);
    }

    private void Start()
    {
        //path = "LeeJuHwan/JsonData/PlayerData";
        Debug.Log(path);
    }

    public void SaveData<T>(T wantData) where T : JData
    {
        var fileName = typeof(T).Name.Remove(0, "J".Length);
        //string data = JsonConvert.SerializeObject(wantdata);
        string data = JsonUtility.ToJson(wantData);
        Debug.Log(data);
        File.WriteAllText($"{path}/{fileName}.json", data);
    }

    public void SaveListData<T>(List<T> wantData) where T : JData
    {
        Debug.Log(wantData);
        var fileName = typeof(T).Name.Remove(0, "J".Length);
        //string data = JsonConvert.SerializeObject(wantData);
        // 이 곳에서 데이터가 모두 소실되어버림

        string data = JsonUtility.ToJson(wantData);
        Debug.Log(data);
        File.WriteAllText($"{path}/{fileName}.json", data);

    }

    //public void LoadData<T>()
    //{
    //    Debug.Log(path);
    //    if (!File.Exists(path + fileName))
    //    {
    //        Debug.Log("파일이 존재하지 않음!");
    //        return;
    //    }
    //    string data = File.ReadAllText(path + fileName);
    //    playerData = FromJson<JPlayerData>(data);
    //}

    public void Load<T>(out T data) where T : JData
    {
        var fileName = typeof(T).Name.Remove(0, "J".Length);

        var json = File.ReadAllText($"{path}/{fileName}.json");

        data = FromJson<T>(json);
    }

    public void LoadList<T>(out List<T> data) where T : JData
    {
        var fileName = typeof(T).Name.Remove(0, "J".Length);

        var json = File.ReadAllText($"{path}/{fileName}.json");

        data = FromListJson<T>(json);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static List<T> FromListJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}
