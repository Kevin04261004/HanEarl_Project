using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class G_VolumeSetting : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private float _value = 0.8f;
    private void OnEnable()
    {
        if (TryGetComponent<Volume>(out volume))
        {

        }
        else
        {
            return;
        }

        volume.profile.TryGet(out ColorLookup colorLookup);

        if (!colorLookup)
        {
            return;
        }

        colorLookup.contribution.value = _value;
    }
    /* 김도윤_끄는 기능 추가 */
    private void OnDisable()
    {
        if (volume.profile.TryGet(out ColorLookup colorLookup))
        {
            colorLookup.contribution.value = 0f;
        }
       
    }
}
