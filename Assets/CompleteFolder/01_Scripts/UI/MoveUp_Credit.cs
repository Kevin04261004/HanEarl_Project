using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUp_Credit : MonoBehaviour
{
    [SerializeField] private float _speed;
    public bool a = true;
    [SerializeField] private GameObject b;
    
    private void Awake()
    {
        StartCoroutine(MoveUp());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !a)
        {
            KLoadingSceneManager.LoadScene("00_TitleScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        a = false;
        b.SetActive(true);
    }

    private IEnumerator MoveUp()
    {
        yield return new WaitForSeconds(3);
        while (a)
        {
            gameObject.transform.position += new Vector3(0, _speed) * Time.deltaTime;
            yield return null;
        }
    }
}
