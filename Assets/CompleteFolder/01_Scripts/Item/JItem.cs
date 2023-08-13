using System.Collections.Generic;
using UnityEngine;

public class JItem : MonoBehaviour
{
    public JItemInstance itemData;
    // Use시 보여줄 스프라이트
    public List<Sprite> itemSprite;
    
    // 인벤토리 슬롯에서 보일 이미지 스프라이트
    public Sprite itemImage;
    
    public void Get()
    {
        itemData.isGet = true;
        itemData.isInvn = true;
        gameObject.SetActive(false);
        G_InventorySystem.Instance.J_AddItem(this);
    }
}
