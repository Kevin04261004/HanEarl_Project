using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G_Slot : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] GameObject backGround;

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
    public void OnPointerEnter(PointerEventData eventData)
    {
        backGround.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backGround.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        G_InventorySystem.Instance.ItemClicked(Jitem);

    }
}
