using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kmark : MonoBehaviour
{
    public bool _needMark;
    [SerializeField] private float _fadeTime = 0.75f;
    [SerializeField] private float _speed = 200;
    private Vector3 _a = new Vector3(0, 0, 1);
    private Coroutine _twinkleRoutine;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_needMark)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0);
            return;
        }

        transform.Rotate(_a * (Time.deltaTime * _speed));
        if (_twinkleRoutine == null)
        {
            _twinkleRoutine = StartCoroutine(TwinkleLoop());
        }
    }
    private IEnumerator TwinkleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeEffect(1, 0));
            yield return StartCoroutine(FadeEffect(0, 1));
        }
    }
    private IEnumerator FadeEffect(float start, float end)
    {
        
        float curTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            if (!_needMark)
            {
                _twinkleRoutine = null;
                yield break;
            }
            curTime += Time.deltaTime;
            percent = curTime / _fadeTime;

            Color color = _spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            _spriteRenderer.color = color;

            yield return null;
        }
    }
}
