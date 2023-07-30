using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCManager : MonoBehaviour
{
    [SerializeField] private float _time = 1.5f;
    [SerializeField] private Material _material;
    [SerializeField] private KFadeManager _fadeManager;
    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _fadeManager = FindObjectOfType<KFadeManager>();
    }

    public void NoneEffectRoutine()
    {
        StartCoroutine(NoneEffectStart());
    }
    public void FirstMeetInteractiveRoutine()
    {
        StartCoroutine(FirstInteractiveStart());
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

    private IEnumerator FirstInteractiveStart()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(1);
        _fadeManager.FadeInRoutine(1);
        KTimeLineManager.Instance.StartTimeLine("04");
    }
}
