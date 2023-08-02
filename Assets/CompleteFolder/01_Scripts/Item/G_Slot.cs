using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G_Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    //[SerializeField]
    //private JItemInstance _item;

    public JItem Jitem;

    // public JItemInstance item
    // {
    //     get { return _item; }
    //     set { 
    //         _item = value; 
    //         if (_item != null)
    //         {
    //             //image.sprite = 
    //             image.color = new Color(1, 1, 1, 1);
    //         }
    //         else
    //         {
    //             image.color = new Color(1, 1, 1, 0);
    //         }
    //     }
    // }

    public void OnPointerClick(PointerEventData eventData)
    {
        G_InventorySystem.Instance.ItemClicked(Jitem);
    }
}
