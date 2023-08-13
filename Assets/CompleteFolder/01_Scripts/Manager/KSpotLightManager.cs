using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KSpotLightManager : MonoBehaviour
{
    [SerializeField] private GameObject _spotLight;
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private float _intensity = 0.01f;
    private float _baseIntensity;
    private void Awake()
    {
        _baseIntensity = _globalLight.intensity;
    }
    
    /* 타임라인에서 건들기. */
    public void SpotLightOnOff(bool on)
    {
        _spotLight.SetActive(on);
        if (on)
        {
            _globalLight.intensity = _intensity;
        }
        else
        {
            _globalLight.intensity = _baseIntensity;
        }
    }
}