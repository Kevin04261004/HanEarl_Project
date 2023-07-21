using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JItemManager : MonoBehaviour
{
    JItemData itemDatas = new JItemData();

    private void Start()
    {
        var itemlist = JDataManager.instance.itemData;
        for (int i = 0; i < itemlist.itemList.Count; i++)
        {
            Debug.Log(itemlist.itemList.Count);
            GameObject item = Instantiate(Resources.Load<GameObject>(itemlist.itemList[i].prefab), itemlist.itemList[i].currentPos, Quaternion.identity);
            item.GetComponent<JItemBase>().itemData = itemlist.itemList[i];
            itemDatas.itemList.Add(item.GetComponent<JItemBase>().itemData);
            //itemDatas.itemList.Add(itemlist.itemList[i]);
            if (itemDatas.itemList[i].isGet)
            {
                item.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(itemDatas.itemList[0].isGet);
            Debug.Log(itemDatas.itemList[0].itemName);
            Debug.Log(itemDatas.itemList[0].index);
            JDataManager.instance.SaveData(itemDatas);
        }
    }
}
