using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JButtonOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.enabled = false;
    }
}
