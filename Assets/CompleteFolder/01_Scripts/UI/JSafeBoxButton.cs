using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JSafeBoxButton : MonoBehaviour
{
    [SerializeField] Text number;

    public int num = 0;
    public void UpNumber()
    {
        if (num >= 9)
            num = 0;
        else
            num++;
        number.text = num.ToString();
    }

    public void DownNumber()
    {
        if (num <= 0)
            num = 9;
        else
            num--;
        number.text = num.ToString();
    }
}
