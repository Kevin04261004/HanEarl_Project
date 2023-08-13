using Cinemachine;
using UnityEngine;

public class KCamManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cm;

    public void PlayerTargetCam_SetActive_Bool(bool isTrue)
    {
        _cm.gameObject.SetActive(isTrue);
    }
}
