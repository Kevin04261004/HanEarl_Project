using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHairDryItem : MonoBehaviour
{
    [SerializeField] private KFadeManager _fadeManager;

    private void Awake()
    {
        _fadeManager = FindObjectOfType<KFadeManager>();
    }

    public void InteractiveRoutine()
    {
        
    }

    public IEnumerator InteractiveCoroutine()
    {
        _fadeManager.FadeInRoutine(1);
        
        KTimeLineManager.Instance.StartTimeLine("07");
    }
}
