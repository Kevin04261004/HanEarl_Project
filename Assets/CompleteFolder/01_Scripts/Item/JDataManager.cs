using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JDataManager : MonoBehaviour
{
    public static JDataManager instance;

    string path;
    
    // jsonData에서 가져온 데이터
    public JPlayerData playerData;
    //public JItemData itemData;
    public GStageSaveData stageData;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
        path = $"{Application.streamingAssetsPath}";
        Load(out playerData);
        //Load(out itemData);
        Load(out stageData);
    }

    public void SaveData<T>(T wantData) where T : JData
    {
        var fileName = typeof(T).Name.Remove(0, "J".Length);
        //string data = JsonConvert.SerializeObject(wantdata);
        string data = JsonUtility.ToJson(wantData);

        File.WriteAllText($"{path}/{fileName}.json", data);
    }

    public void SaveListData<T>(List<T> wantData) where T : JData
    {

        var fileName = typeof(T).Name.Remove(0, "J".Length);
        //string data = JsonConvert.SerializeObject(wantData);
        // 이 곳에서 데이터가 모두 소실되어버림

        string data = JsonUtility.ToJson(wantData);

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
        //var json = Resources.Load<TextAsset>($"{path}/{fileName}.json").ToString();
        
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
