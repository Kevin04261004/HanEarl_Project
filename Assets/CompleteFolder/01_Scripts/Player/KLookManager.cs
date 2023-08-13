using System.Collections;
using Cinemachine;
using UnityEngine;

public class KLookManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cm;
    [SerializeField] private float _baseSight;
    [SerializeField] private float _changeSight = 40;
    [SerializeField] private float _speed = 0.4f;
    private KPlayerManager _playerManager;
    private void Awake()
    {
        _playerManager = FindObjectOfType<KPlayerManager>();
        TryGetComponent(out CinemachineVirtualCamera cam);
        _cm = cam;
        _baseSight = _cm.m_Lens.FieldOfView;
    }

    private void Update()
    {
        /* 꾹 누르면 빠르게 줌인이 되고 그 키를 때면 줌인이 안되게 만들자. */
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SightKey]))
        {
            if (_playerManager._inputKey != -1)
            {
                return;
            }
            StopCoroutine(nameof(ZoomOut));
            StartCoroutine(nameof(ZoomIn));
        }

        if (!Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.SightKey]))
        {
            return;
        }
        StopCoroutine(nameof(ZoomIn));
        StartCoroutine(nameof(ZoomOut));
    }

    private IEnumerator ZoomIn()
    {
        var temp = _cm.m_Lens.FieldOfView;
        while (_cm.m_Lens.FieldOfView > _changeSight)
        {
            temp -= Time.deltaTime / _speed;
            _cm.m_Lens.FieldOfView = temp;
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        var temp = _cm.m_Lens.FieldOfView;
        while (_cm.m_Lens.FieldOfView < _baseSight)
        {
            temp += Time.deltaTime / _speed;
            _cm.m_Lens.FieldOfView = temp;
            yield return null;
        }
    }
}
