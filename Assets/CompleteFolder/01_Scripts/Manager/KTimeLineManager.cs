using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class KTimeLineManager : MonoBehaviour
{
    [SerializeField] private Transform _timeLineParent;
    [SerializeField] private GameObject[] _timeLines;
    public static KTimeLineManager Instance;
    private KFadeManager _fadeManager;
    private string _curTimeLine;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
        }
        else
        {   
            Destroy(this.gameObject);
        }

        if (!_timeLineParent)
        {
            Debug.Assert(_timeLineParent,"타임라인 부모오브젝트가 존재하지 않습니다.");
        }
        else
        {
            _timeLines = new GameObject[_timeLineParent.childCount];
            for (int i = 0; i < _timeLineParent.childCount; ++i)
            {
                _timeLines[i] = _timeLineParent.GetChild(i).gameObject;
            }
        }

        _fadeManager = FindObjectOfType<KFadeManager>();

    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SkipKey]) 
            && _curTimeLine != String.Empty 
            && _curTimeLine != "06"// Normal Ending;
            && _curTimeLine != "11"// Real Ending;
            )
        {
            SkipTimeLine(_curTimeLine);
        }
    }
    
    public void StartTimeLine(string name)
    {
        foreach (var t in _timeLines)
        {
            t.SetActive(false);
            if (t.name == name)
            {
                _curTimeLine = name;
                t.SetActive(true);
            }
        }
    }

    public void SkipTimeLine(string name)
    {
        foreach (var t in _timeLines)
        {
            if (t.name == name)
            {
                if(t.TryGetComponent(out PlayableDirector playableDirector))
                {
                    _curTimeLine = String.Empty;
                    playableDirector.time = playableDirector.duration - 1f;
                }
            }
        }
    }

    public void GameObjectTSetActiveFalse(GameObject go)
    {
        go.SetActive(false);
    }
    public void GameObjectTSetActiveTrue(GameObject go)
    {
        go.SetActive(true);
    }

    public void StartTimeLine10Routine()
    {
        StartCoroutine(StartTimeLine10());
    }
    private IEnumerator StartTimeLine10()
    {
        _fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        KTimeLineManager.Instance.StartTimeLine("10");
        _fadeManager.FadeInRoutine();
    }
}
