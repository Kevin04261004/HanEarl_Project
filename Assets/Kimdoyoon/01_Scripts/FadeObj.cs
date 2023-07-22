using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeObj : MonoBehaviour
{
    [SerializeField] private Tilemap t;
    [SerializeField] private Color fadeInColor;
    [SerializeField] private Color baseColor;
    [SerializeField] private float time = 0.3f;
    private void Awake()
    {
        t = GetComponent<Tilemap>();
        baseColor = t.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StopCoroutine("FadeOut");
            StartCoroutine("FadeIn");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine("FadeIn");
            StartCoroutine("FadeOut");
        }
    }
    private IEnumerator FadeIn()
    {
        WaitForSeconds _time = new WaitForSeconds(time/100);
        float temp = (t.color.a - fadeInColor.a)/100;
        Color c = new Color(0,0,0, temp);
        for(int i = 0; i< 100; ++i)
        {
            t.color -= c;
            yield return _time;
        }
    }
    private IEnumerator FadeOut()
    {
        WaitForSeconds _time = new WaitForSeconds(time / 100);
        float temp = (baseColor.a - t.color.a) / 100;
        Color c = new Color(0, 0, 0, temp);
        for (int i = 0; i < 100; ++i)
        {
            t.color += c;
            yield return _time;
        }
    }
}
