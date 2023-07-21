using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class JItemBase : MonoBehaviour
{
    public JItemInstance itemData;

    private void Start()
    {
        Debug.Log(itemData.itemName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 아이템을 얻었을 경우
        itemData.isGet = true;
        gameObject.SetActive(false);
    }
}
