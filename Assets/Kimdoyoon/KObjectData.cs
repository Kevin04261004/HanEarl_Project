using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Useable_Item,
    Eternal_Item,
}

public class KObjectData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public void UseItem()
    {

    }
}
