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
    // ���� ��
    public string itemName;
    public string dialog;
    public string prefab;
    public int index;

    // ���ϴ� ��
    public bool isGet;
    public bool isInvn; // �κ��丮�� �����ϴ���
    public Vector3 currentPos;
}