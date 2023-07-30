using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KSpotLightManager : MonoBehaviour
{
    [SerializeField] private GameObject _spotLight;
    [SerializeField] private Light2D _globalLight;
    private float _baseIntensity; 
    private void Awake()
    {
        _baseIntensity = _globalLight.intensity;
        _spotLight = transform.GetChild(0).gameObject;
    }
    
    /* 타임라인에서 건들기. */
    public void SpotLightOnOff(bool on)
    {
        _spotLight.SetActive(on);
        if (on)
        {
            _globalLight.intensity = 0;
        }
        else
        {
            _globalLight.intensity = _baseIntensity;
        }
    }
}