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
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private void Awake()
    {
        _playerManager = FindObjectOfType<KPlayerManager>();
    }

    /* FADE IN */
    public void FadeInRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        StopCoroutine(nameof(FadeOut));
        StartCoroutine(nameof(FadeIn),time);
    }

    public void FadeOutRoutine(float time= 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        StopCoroutine(nameof(FadeIn));
        StartCoroutine(nameof(FadeOut),time);
    }

    public void FadeIn_ImageSetActiveTrueRoutine(float time= 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        StartCoroutine(nameof(FadeIn_ImageSetActiveTrue),time);
    }
    public void FadeOut_ImageSetActiveTrueRoutine(float time= 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        StartCoroutine(nameof(FadeOut_ImageSetActiveTrue),time);
    }
    private IEnumerator FadeIn(float time = 1)
    {
        _fade_Image.color = A_1;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime / time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeOut(float time = 1)
    {
        _fade_Image.color = A_0;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime / time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeIn_ImageSetActiveTrue(float time = 1)
    {
        _fade_Image.color = A_1;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime / time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
    }
    private IEnumerator FadeOut_ImageSetActiveTrue(float time = 1)
    {
        _fade_Image.color = A_0;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime / time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
    }
}