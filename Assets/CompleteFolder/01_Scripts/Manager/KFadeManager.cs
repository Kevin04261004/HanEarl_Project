using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KFadeManager : MonoBehaviour
{
    [SerializeField]
    private KPlayerManager _playerManager;
    [field: SerializeField] public Image _fade_Image { get; private set; }
    [field: SerializeField] public Image _flash_Image { get; private set; }
    [SerializeField]
    private Animator _playerAnimator;
    private Color A_1 = new Color(0, 0, 0, 1);
    private Color A_0 = new Color(0, 0, 0, 0);
    private Color White_A_1 = new Color(1, 1, 1, 1);
    private Color White_A_0 = new Color(1, 1, 1, 0);
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private Coroutine _fadeCoroutine;

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
        KGameManager.Instance._canSkip = false;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeIn());
    }
    /* FADE OUT */
    public void FadeOutRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        KGameManager.Instance._canSkip = false;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeOut());
    }
    /* FLASH IN */
    public void FlashInRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _flash_Image.gameObject.SetActive(true);
        KGameManager.Instance._canSkip = false;
        KGameManager.Instance._canInput = false;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FlashIn());
    }
    /* FLASH OUT */
    public void FlashOutRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _flash_Image.gameObject.SetActive(true);
        KGameManager.Instance._canSkip = false;
        KGameManager.Instance._canInput = false;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FlashOut());
    }
    public void FadeIn_ImageSetActiveTrueRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        KGameManager.Instance._canSkip = false;

        _fadeCoroutine = StartCoroutine(FadeIn_ImageSetActiveTrue(time));
    }
    public void FadeOut_ImageSetActiveTrueRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _fade_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        KGameManager.Instance._canSkip = false;

        _fadeCoroutine = StartCoroutine(FadeOut_ImageSetActiveTrue(time));
    }
    public void FlashIn_ImageSetActiveTrueRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _flash_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        KGameManager.Instance._canSkip = false;

        _fadeCoroutine = StartCoroutine(FlashIn_ImageSetActiveTrue(time));
    }
    public void FlashOut_ImageSetActiveTrueRoutine(float time = 1)
    {
        _playerManager.ResetInputKey();
        _playerAnimator.SetBool(IsWalking, false);
        _flash_Image.gameObject.SetActive(true);
        KGameManager.Instance._canInput = false;
        KGameManager.Instance._canSkip = false;

        _fadeCoroutine = StartCoroutine(FlashOut_ImageSetActiveTrue(time));
    }
    public void StopAllFadingRoutines()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);
    }
    public void DeactivateFadeImage()
    {
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeIn(float time = 1)
    {
        _fade_Image.color = A_1;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime/time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        KGameManager.Instance._canSkip = true;
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeOut(float time = 1)
    {
        _fade_Image.color = A_0;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime/time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        KGameManager.Instance._canSkip = true;
        _fade_Image.gameObject.SetActive(false);
    }
    private IEnumerator FadeIn_ImageSetActiveTrue(float time = 1)
    {
        _fade_Image.color = A_1;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime/time;
            _fade_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        KGameManager.Instance._canSkip = true;
    }
    private IEnumerator FadeOut_ImageSetActiveTrue(float time = 1)
    {
        _fade_Image.color = A_0;
        Color tempColor = _fade_Image.color;
        while (_fade_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime/time;
            _fade_Image.color = tempColor;
            yield return null;
        }
    }
    private IEnumerator FlashIn(float time = 1)
    {
        _flash_Image.color = White_A_1;
        Color tempColor = _flash_Image.color;
        while (_flash_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime/time;
            _flash_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        KGameManager.Instance._canSkip = true;
        _flash_Image.gameObject.SetActive(false);
    }
    private IEnumerator FlashOut(float time = 1)
    {
        _flash_Image.color = White_A_0;
        Color tempColor = _flash_Image.color;
        while (_flash_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime/time;
            _flash_Image.color = tempColor;
            yield return null;
        }

        KGameManager.Instance._canInput = true;
        KGameManager.Instance._canSkip = true;
        _flash_Image.gameObject.SetActive(false);
    }
    private IEnumerator FlashIn_ImageSetActiveTrue(float time = 1)
    {
        _flash_Image.color = new Color(_flash_Image.color.r, _flash_Image.color.g, _flash_Image.color.b, 1);
        Color tempColor = _flash_Image.color;
        while (_flash_Image.color.a > 0)
        {
            tempColor.a -= Time.deltaTime/time;
            _flash_Image.color = tempColor;
            yield return null;
        }

    }
    private IEnumerator FlashOut_ImageSetActiveTrue(float time = 1)
    {
        _flash_Image.color = new Color(_flash_Image.color.r, _flash_Image.color.g, _flash_Image.color.b, 0);
        Color tempColor = _flash_Image.color;
        while (_flash_Image.color.a < 1)
        {
            tempColor.a += Time.deltaTime/time;
            _flash_Image.color = tempColor;
            yield return null;
        }
    }
}
