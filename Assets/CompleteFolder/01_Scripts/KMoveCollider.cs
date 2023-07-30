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
        if (!_targetPos)
        {
            return;
        }

        float time = 1;
        if (other.CompareTag("Player"))
        {
            _fadeManager.FadeInRoutine(time);
            if(other.TryGetComponent(out KPlayerManager player))
            {
                player._isMoving = false;
                player.StopMoveWithTimeRoutine(time, _targetPos.position);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out KEnemy enemy))
            {
                enemy.StopMoveWithTimeRoutine(time,_targetPos.position);
            }
            
        }
    }
}