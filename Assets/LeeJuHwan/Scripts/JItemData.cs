using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JItemData : JData
{
    public List<JItemInstance> itemList = new List<JItemInstance>();
}

[Serializable]
public class JItemInstance
{
    // 고정 값
    public string itemName;
    public string dialog;
    public string prefab;
    public int index;

    // 변하는 값
    public bool isGet;
    public Vector3 currentPos;
}