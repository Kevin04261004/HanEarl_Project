using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActFour_Trigger : MonoBehaviour
{
    [SerializeField] private KFadeManager _fadeManager;
    private void Awake()
    {
        _fadeManager = FindObjectOfType<KFadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        StartCoroutine(BadEndingAStart());
    }
    
    private IEnumerator BadEndingAStart()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        _fadeManager.FadeInRoutine(2);
        KTimeLineManager.Instance.StartTimeLine("10");
        gameObject.SetActive(false);
    }
}
