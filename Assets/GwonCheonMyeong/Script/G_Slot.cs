using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G_Slot : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField]
    private JItemInstance _item;

    public JItemInstance item
    {
        get { return _item; }
        set { 
            _item = value; 
            if (_item != null)
            {
                //image.sprite = 
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
