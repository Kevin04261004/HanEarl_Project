using System;
using UnityEngine;

public class KAfterSchool : MonoBehaviour
{
    private KInteractiveObject _interactiveObject;

    private void Awake()
    {
        _interactiveObject = GetComponent<KInteractiveObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _interactiveObject.Interactive();
        }
    }
}