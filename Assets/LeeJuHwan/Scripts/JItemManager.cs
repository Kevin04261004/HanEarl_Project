using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JItemManager : MonoBehaviour
{
    public JItemData itemDatas = new JItemData();

    public static JItemManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        var itemlist = JDataManager.instance.itemData;
        for (int i = 0; i < itemlist.itemList.Count; i++)
        {
            Debug.Log(itemlist.itemList.Count);
            GameObject item = Instantiate(Resources.Load<GameObject>(itemlist.itemList[i].prefab), itemlist.itemList[i].currentPos, Quaternion.identity);
            item.GetComponent<JItem>().itemData = itemlist.itemList[i];
            itemDatas.itemList.Add(item.GetComponent<JItem>().itemData);
            //itemDatas.itemList.Add(itemlist.itemList[i]);
            if (itemDatas.itemList[i].isGet)
            {
                item.SetActive(false);
            }
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JDataManager.instance.SaveData(itemDatas);
        }
    }
}
