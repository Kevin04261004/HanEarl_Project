using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBManager : MonoBehaviour
{
    [SerializeField] private float _time = 1.5f;
    [SerializeField] private KFadeManager _fadeManager;
    
    private void Awake()
    {
        _fadeManager = FindObjectOfType<KFadeManager>();
    }
    
    public void FirstMeetInteractiveRoutine()
    {
        StartCoroutine(FirstInteractiveStart());
    }
    
    private IEnumerator FirstInteractiveStart()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(2);
        _fadeManager.FadeInRoutine(1);
        KTimeLineManager.Instance.StartTimeLine("05");
    }
}
