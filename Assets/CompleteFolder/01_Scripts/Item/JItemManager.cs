using UnityEngine;

public class JItemManager : MonoBehaviour
{

    // 게임 중 실시간으로 모든 아이템들의 데이터를 관리할 변수(저장 기능을 사용할 경우 이 변수에 있는 데이터들을 저장함)
    public JItemData itemDatas = new JItemData();

    //public static JItemManager instance;

    private void Start()
    {
        // if (instance == null)
        //     instance = this;
        // else
        //     Destroy(this);

        //Initialize();
    }

    //public void Initialize()
    //{
    //    // jsonData에 있던 데이터
    //    var itemlist = JDataManager.instance.itemData;
    //    for (int i = 0; i < itemlist.itemList.Count; i++)
    //    {
    //        var item = Instantiate(Resources.Load<GameObject>(itemlist.itemList[i].prefab),
    //            itemlist.itemList[i].currentPos, Quaternion.identity).GetComponent<JItem>();
    //        item.itemData = itemlist.itemList[i];
    //        itemDatas.itemList.Add(item.itemData);
    //        //itemDatas.itemList.Add(itemlist.itemList[i]);
    //        if (itemDatas.itemList[i].isGet)
    //        {
    //            item.gameObject.SetActive(false);
    //        }

    //        if (itemDatas.itemList[i].isInvn)
    //        {
    //            G_InventorySystem.Instance.J_AddItem(item);
    //        }
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //JDataManager.instance.SaveData(itemDatas);
    //    }
    //}
}
