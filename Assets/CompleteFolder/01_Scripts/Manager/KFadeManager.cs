using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KFadeManager : MonoBehaviour
{
    [SerializeField] private KPlayerManager _playerManager;
    [SerializeField] private Image _fade_Image;
    [SerializeField] private Animator _playerAnimator;
    private Color A_1 = new Color(0, 0, 0, 1);
    private Color A_0 = new Color(0, 0, 0, 0);
    [SerializeField] private float _fadeTime = 0.5f;

    private void Awake()
    {
        _playerManager = FindObjectOfType<KPlayerManager>();
    }

    /* FADE IN */
    public void FadeInRoutine(bool canInputUntilFadeIn = false)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool("isWalking", false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = canInputUntilFadeIn;
        StopCoroutine(nameof(FadeOut));
        StartCoroutine(nameof(FadeIn));
    }

    public void FadeOutRoutine(bool canInputUntilFadeOut = false)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool("isWalking", false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = canInputUntilFadeOut;
        StopCoroutine(nameof(FadeIn));
        StartCoroutine(nameof(FadeOut));
    }

    public void FadeInOutAllRoutine(bool canInputUntilFadeInOut = false)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool("isWalking", false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = canInputUntilFadeInOut;
        StartCoroutine(nameof(FadeInOutAll));
    }
    private IEnumerator FadeIn()
    {
        _fade_Image.color = A_1;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime / _fadeTime;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeOut()
    {
        _fade_Image.color = A_0;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime / _fadeTime;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeInOutAll()
    {
        _fade_Image.color = A_1;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime / _fadeTime;
            _fade_Image.color = tempColor;
            yield return null;
        }
        while (_fade_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime / _fadeTime;
            _fade_Image.color = tempColor;
            yield return null;
        }
        
        KGameManager.Instance._canInput = true;
        _fade_Image.gameObject.SetActive(false);
    }
}