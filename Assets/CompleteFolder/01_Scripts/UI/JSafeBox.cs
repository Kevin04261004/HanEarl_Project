using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JSafeBox : MonoBehaviour
{
    private JSafeBoxButton[] nums;
    string currentNum = "";
    private void Awake()
    {
        nums = transform.gameObject.GetComponentsInChildren<JSafeBoxButton>();
    }

    //public int CheckPassword()
    //{
    //    currentNum = "";
    //    for(int i = 0; i < 4;i++)
    //    {
    //        currentNum += nums[i].num.
    //    }
    //}
}
