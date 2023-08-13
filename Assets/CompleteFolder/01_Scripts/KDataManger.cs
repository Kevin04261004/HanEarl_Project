using System.IO;
using UnityEngine;

public class PlayerData
{
    public string _name;
    public int _stage = 0;
}

public class KDataManger : MonoBehaviour
{
    public static KDataManger Instance;
    public string _path;
    public int _nowSlot;
    public PlayerData _nowPlayer = new PlayerData();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        _path = Path.Combine(Application.persistentDataPath, "Ksave");
        print(_path);
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(_nowPlayer);
        File.WriteAllText(_path+ _nowSlot, data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(_path + _nowSlot);
        _nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
    public void DataClear()
    {
        _nowSlot = -1;
        _nowPlayer = new PlayerData();
    }
}
