using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class G_VolumeSetting : MonoBehaviour
{
    [SerializeField] private Volume volume;

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
            Debug.Log("컬러 변환 X");
            return;
        }

        colorLookup.contribution.value = 0.7f;
    }
}
