using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public struct KAct_TimeLine
{
    public string actName;
    public GameObject[] actObj;
};

public class KTimeLineManager : MonoBehaviour
{
    [SerializeField] private Transform _timeLineParent;
    [SerializeField] private GameObject[] _timeLines;
    public static KTimeLineManager Instance;
    private KFadeManager _fadeManager;
    private string _curTimeLine;
    [SerializeField] private PlayableDirector[] pd;
    public KAct_TimeLine[] _actTimeLine;
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
            pd = new PlayableDirector[_timeLineParent.childCount];
            for (int i = 0; i < _timeLineParent.childCount; ++i)
            {
                _timeLines[i] = _timeLineParent.GetChild(i).gameObject;
                pd[i] = _timeLineParent.GetChild(i).GetComponent<PlayableDirector>();
            }
        }
        
        _fadeManager = FindObjectOfType<KFadeManager>();

    }
    
    private void Update()
    {
        if (!Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SkipKey]))
        {
            return;
        }
        foreach (var v in pd)
        {
            if (!v.gameObject.activeSelf)
            {
                continue;
            }
            _curTimeLine = v.gameObject.name;
        }
        
        foreach (var playedAct in JDataManager.instance.stageData.playedActName)
        {
            KAct_TimeLine temp = new KAct_TimeLine();
            bool hasPlayedAct = false;
            foreach (var a in _actTimeLine)
            {
                if (a.actName != playedAct)
                {
                    continue;
                }
                foreach (var v in a.actObj)
                {
                    if (_curTimeLine != v.name)
                    {
                        continue;
                    }
                    hasPlayedAct = true;
                    temp = a;
                }
            }

            if (!hasPlayedAct)
            {
                continue;
            }
            foreach (var actObj in temp.actObj)
            {
                if (actObj.name != _curTimeLine)
                {
                    continue;
                }
                SkipTimeLine(_curTimeLine);
            }
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
                    playableDirector.time = playableDirector.duration - 2f;
                }
            }
        }
        _fadeManager.StopAllFadingRoutines();
        _fadeManager.DeactivateFadeImage();
        
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
