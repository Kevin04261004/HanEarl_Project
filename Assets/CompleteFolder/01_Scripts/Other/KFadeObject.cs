using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KFadeObject : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Color _fadeInColor;
    [SerializeField] private Color _baseColor;
    [SerializeField] private float _fadeTime = 0.3f;
    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _baseColor = _tilemap.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        StopCoroutine(nameof(FadeOut));
        StartCoroutine(nameof(FadeIn));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        StopCoroutine(nameof(FadeIn));
        StartCoroutine(nameof(FadeOut));
    }
    private IEnumerator FadeIn()
    {
        Color tempColor = _tilemap.color;
        while (tempColor.a > _fadeInColor.a)
        {
            tempColor.a -= Time.deltaTime / _fadeTime;
            _tilemap.color = tempColor;
            yield return null;
        }
    }
    private IEnumerator FadeOut()
    {
        Color tempColor = _tilemap.color;
        while (tempColor.a < _baseColor.a)
        {
            tempColor.a += Time.deltaTime / _fadeTime;
            _tilemap.color = tempColor;
            yield return null;
        }
    }
}