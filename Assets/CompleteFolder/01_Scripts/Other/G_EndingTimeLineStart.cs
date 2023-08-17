using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_EndingTimeLineStart : MonoBehaviour
{
    [SerializeField] private KFadeManager _fadeManager;

    private void Awake()
    {
        _fadeManager = FindObjectOfType<KFadeManager>();
    }

    public void HairDry()
    {
        StartCoroutine(HairDry_InteractiveCoroutine());
    }

    public void Rope()
    {
        StartCoroutine(Rope_InteractiveCoroutine());
    }

    public void Rooftop()
    {
        StartCoroutine(Rooftop_InteractiveCoroutine());
    }

    private IEnumerator HairDry_InteractiveCoroutine()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        _fadeManager.FadeInRoutine(1);
        G_DifurcationManager.Instance.CallEnding("BadEndingD");
        Debug.Log("TimeLineStart");
        KTimeLineManager.Instance.StartTimeLine("07");
    }

    private IEnumerator Rope_InteractiveCoroutine()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(1);
        _fadeManager.FadeInRoutine(1);
        G_DifurcationManager.Instance.CallEnding("BadEndingE");
        KTimeLineManager.Instance.StartTimeLine("08");
    }

    private IEnumerator Rooftop_InteractiveCoroutine()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        _fadeManager.FadeInRoutine(1);
        G_DifurcationManager.Instance.CallEnding("BadEndingC");
        KTimeLineManager.Instance.StartTimeLine("09");
    }
}
