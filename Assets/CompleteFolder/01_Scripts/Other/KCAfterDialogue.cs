using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCAfterDialogue : MonoBehaviour
{
    [SerializeField] private GameObject BeforeObj;
    [SerializeField] private GameObject NewObj;
    [SerializeField] private GameObject BeforeObj2;
    [SerializeField] private GameObject NewObj2;
    public void Used()
    {
        BeforeObj.SetActive(false);
        NewObj.SetActive(true);
        BeforeObj2.SetActive(false);
        NewObj2.SetActive(true);
    }
}
