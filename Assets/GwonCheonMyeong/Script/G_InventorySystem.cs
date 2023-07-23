using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_InventorySystem : MonoBehaviour
{
    public static G_InventorySystem Instance;

    public List<JItemInstance> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private G_Slot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<G_Slot>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);


    }

    private void Start()
    {
        LoadItem();

        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }

        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void LoadItem()
    {
        int itemCount = JItemManager.instance.itemDatas.itemList.Count;
        if (itemCount > 0)
        {
            for (int i = 0; i < itemCount; i++)
            {
                AddItem(JItemManager.instance.itemDatas.itemList[i]);
            }   
        }

    }

    public void AddItem(JItemInstance _item)
    {
        if(items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            Debug.Log("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }

    public void DestroyItem(JItemInstance _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemName == _item.itemName)
            {
                items.Remove(_item);
                Debug.Log($"{i}¹øÂ° ½½·Ô ¾ÆÀÌÅÛ »èÁ¦µÊ{_item}");
                break;
            }
        }
        FreshSlot();
    }
}
