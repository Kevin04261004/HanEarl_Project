using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;



public class KTimeLineManager : MonoBehaviour
{
    [SerializeField] private Transform _timeLineParent;
    [SerializeField] private GameObject[] _timeLines;
    public static KTimeLineManager Instance;
    [SerializeField] private bool _canSkip;
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
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.SkipKey]) && _curTimeLine != String.Empty)
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
                    playableDirector.time = playableDirector.duration - 0.1f;
                }
            }
        }
    }

    public void GameObjectTSetActiveFalse(GameObject go)
    {
        go.SetActive(false);
    }
}
