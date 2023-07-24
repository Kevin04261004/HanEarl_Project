using System;
using UnityEngine;

public class KMoveCollider : MonoBehaviour
{
    [SerializeField] private Transform _targetPos;
    [SerializeField] private KFadeManager _fadeManager;

    private void Awake()
    {
        _fadeManager = FindObjectOfType<KFadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_targetPos || !other.CompareTag("Player"))
        {
            return;
        }
        _fadeManager.FadeInRoutine();
        other.transform.position = _targetPos.position;
        other.TryGetComponent(out KPlayerManager player);
        player._isMoving = false;
    }
}