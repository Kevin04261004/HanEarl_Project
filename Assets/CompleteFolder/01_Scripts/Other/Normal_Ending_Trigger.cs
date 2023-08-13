using System.Collections;
using UnityEngine;

public class Normal_Ending_Trigger : MonoBehaviour
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
        StartCoroutine(NormalEndingStart());
    }
    
    private IEnumerator NormalEndingStart()
    {
        _followEnemy.SetActive(false);
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        _fadeManager.FadeInRoutine(2);
        KTimeLineManager.Instance.StartTimeLine("06");
        G_DifurcationManager.Instance.CallEnding("NormalEnding");
        gameObject.SetActive(false);
    }
}
