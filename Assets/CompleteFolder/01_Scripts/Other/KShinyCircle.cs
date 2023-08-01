using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KShinyCircle : MonoBehaviour
{
    [SerializeField] private float _maxSize;
    [SerializeField] private float _time;
    private Vector3 _curSize;

    public void SizeUpRoutine()
    {
        gameObject.SetActive(true);
        StartCoroutine(SizeUp());
    }
    
    private IEnumerator SizeUp()
    {
        float temp = gameObject.transform.localScale.x;
        while (gameObject.transform.localScale.x <= _maxSize)
        {
            temp += Time.deltaTime / _time;
            gameObject.transform.localScale = new Vector3(temp, temp, temp);
            yield return null;   
        }
        gameObject.SetActive(false);
    }
}
