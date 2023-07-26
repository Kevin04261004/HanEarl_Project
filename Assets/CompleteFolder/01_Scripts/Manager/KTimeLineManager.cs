using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KTimeLineManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _timeLines;
    public static KTimeLineManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
        }
        else
        {   
            Destroy(this.gameObject);
        }
    }
    public void StartTimeLine(string name)
    {
        foreach (var t in _timeLines)
        {
            t.SetActive(false);
            if (t.name == name)
            {
                t.SetActive(true);
            }
        }
    }
}
