using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRooftopFence : MonoBehaviour
{
    [SerializeField] private KFadeManager _fadeManager;

    private void Awake()
    {
        _fadeManager = FindObjectOfType<KFadeManager>();
    }

    public void InteractiveRoutine()
    {
        StartCoroutine(InteractiveCoroutine());
    }

    private IEnumerator InteractiveCoroutine()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        _fadeManager.FadeInRoutine(1);
        KTimeLineManager.Instance.StartTimeLine("09");
    }
}
