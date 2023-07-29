using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCManager : MonoBehaviour
{
    [SerializeField] private float _time = 1.5f;
    [SerializeField] private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    public void NoneEffectRoutine()
    {
        StartCoroutine(NoneEffectStart());
    }

    private IEnumerator NoneEffectStart()
    {
        float temp = _material.GetFloat("_SplitValue");
        while (_material.GetFloat("_SplitValue") > 0)
        {
            temp -= Time.deltaTime / _time;
            _material.SetFloat("_SplitValue", temp);
            yield return null;
        }
        _material.SetFloat("_SplitValue", 2);
        gameObject.SetActive(false);
    }
    
}
