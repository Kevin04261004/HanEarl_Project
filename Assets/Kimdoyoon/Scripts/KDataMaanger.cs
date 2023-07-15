using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    public string name;
    public int level = 1;

}

public class KDataMaanger : MonoBehaviour
{
    public string path;
    public int nowSlot;

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        
    }
}
