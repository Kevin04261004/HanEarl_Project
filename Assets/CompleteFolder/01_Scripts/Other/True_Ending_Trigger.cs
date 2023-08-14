using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class True_Ending_Trigger : MonoBehaviour
{
    [SerializeField] private KFadeManager _fadeManager;
    [SerializeField] private GameObject _followEnemy;

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
        StartCoroutine(TrueEndingStart());
    }

    private IEnumerator TrueEndingStart()
    {
        G_DifurcationManager.Instance.CallEnding("TrueEnding");
        _followEnemy.SetActive(false);
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        _fadeManager.FadeInRoutine(2);
        KTimeLineManager.Instance.StartTimeLine("11");
        gameObject.SetActive(false);
    }
}
