using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G_InventorySystem : MonoBehaviour
{
    public static G_InventorySystem Instance;
    [SerializeField]
    private Text currentItemName;

    //public List<JItemInstance> items;

    private bool _jcanInput = true;

    private JItem _currentItem;

    [SerializeField] private Transform slotParent;

    [SerializeField] private Transform buttons;
    [SerializeField] private G_Slot[] slots;

    [SerializeField] private Image itemImage;
    [SerializeField] private Image inven;

    private List<G_Slot> J_slots = new List<G_Slot>();

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
    }

    private void Update()
    {
        if (!_jcanInput)
            return;

        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.InventoryKey]))
        {
            if (inven.gameObject.activeSelf)
            {
                inven.gameObject.SetActive(false);
                buttons.gameObject.SetActive(false);
                KGameManager.Instance.J_GameContinue();
            }
            else
            {
                if (KGameManager.Instance._canInput && KGameManager.Instance._jcanInput)
                {
                    inven.gameObject.SetActive(true);
                    currentItemName.text = "";
                    KGameManager.Instance.J_GamePause();
                }
            }
        }
    }


    private void Start()
    {
        //LoadItem();

        //J_LoadItem();
        //FreshSlot();
    }

    public void J_AddItem(JItem wantItem)
    {
        var slot = Instantiate(Resources.Load<GameObject>("Prefab/slot"), Vector2.zero, Quaternion.identity,
            transform.Find("InventoryImage/Scroll View/Viewport/Content")).GetComponent<G_Slot>();
        slot.Jitem = wantItem;
        slot.GetComponent<Image>().sprite = slot.Jitem.itemImage;
        J_slots.Add(slot);
    }

    // public void FreshSlot()
    // {
    //     int i = 0;
    //     for (; i < items.Count && i < slots.Length; i++)
    //     {
    //         slots[i].item = items[i];
    //     }
    //
    //     //이건 뭐지 이해가 안됨
    //     for (; i < slots.Length; i++)
    //     {
    //         slots[i].item = null;
    //     }
    // }

    // private void J_LoadItem()
    // {
    //     foreach (var jitems in JItemManager.instance.itemDatas.itemList)
    //     {
    //         if (jitems.isInvn)
    //         {
    //             Debug.Log("슬롯 생성");
    //             JItem item = new JItem();
    //             item.itemData = jitems;
    //             J_AddItem(jitems);
    //         }
    //     }
    // }

    // public void LoadItem()
    // {
    //     int itemCount = JItemManager.instance.itemDatas.itemList.Count;
    //     if (itemCount > 0)
    //     {
    //         for (int i = 0; i < itemCount; i++)
    //         {
    //             AddItem(JItemManager.instance.itemDatas.itemList[i]);
    //         }   
    //     }
    //
    // }

    // public void AddItem(JItemInstance _item)
    // {
    //     if(items.Count < slots.Length)
    //     {
    //         items.Add(_item);
    //         FreshSlot();
    //     }
    //     else
    //     {
    //         Debug.Log("슬롯이 가득 차 있습니다.");
    //     }
    // }

    // public void DestroyItem(JItemInstance _item)
    // {
    //     for (int i = 0; i < slots.Length; i++)
    //     {
    //         if (slots[i].item != null && slots[i].item.itemName == _item.itemName)
    //         {
    //             items.Remove(_item);
    //             Debug.Log($"{i}번째 슬롯 아이템 삭제됨{_item}");
    //             break;
    //         }
    //     }
    //     FreshSlot();
    // }

    public void ItemClicked(JItem wantitem)
    {
        _currentItem = wantitem;
        currentItemName.text = _currentItem.itemData.itemName;
        buttons.gameObject.SetActive(true);
    }

    // 사용 버튼
    public void Use()
    {
        if (_currentItem.itemSprite.Count <= 0)
        {
            Debug.Log("사용할 수 없는 아이템입니다.");
            return;
        }

        StartCoroutine(NextItemSprite(_currentItem.itemSprite));
    }

    IEnumerator NextItemSprite(List<Sprite> wantSprite)
    {
        _jcanInput = false;
        itemImage.color = new Color(1, 1, 1, 0);
        itemImage.gameObject.SetActive(true);
        foreach (var iSprite in wantSprite)
        {
            itemImage.sprite = iSprite;
            float itemA = 0;
            while (itemA <= 1)
            {
                itemA += 0.05f;
                itemImage.color = new Color(1, 1, 1, itemA);
                yield return new WaitForSeconds(0.02f);
            }

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    break;
                }
                yield return null;
            }

            while (itemA >= 0)
            {
                itemA -= 0.05f;
                itemImage.color = new Color(1, 1, 1, itemA);
                yield return new WaitForSeconds(0.02f);
            }

            yield return null;
        }

        itemImage.gameObject.SetActive(false);
        _jcanInput = true;

        yield return null;
    }


    // 조사 버튼
    public void Examine()
    {
    }
}