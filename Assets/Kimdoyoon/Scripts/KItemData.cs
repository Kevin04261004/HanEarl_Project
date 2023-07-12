using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Item",menuName ="new Item")]
public class KItemData : ScriptableObject
{
    public string itemName;
    public int itemId;
    
    public string itemDesc;
}
