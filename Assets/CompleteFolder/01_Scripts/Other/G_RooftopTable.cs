using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_RooftopTable : MonoBehaviour
{
    [SerializeField] private KFadeManager fadeManager;
    [SerializeField] private GameObject b_obj;
    [SerializeField] private Transform target_Pos;
    [SerializeField] private Collider2D _other;
    [SerializeField] private Coroutine a;
    private void Awake()
    {
        fadeManager = FindObjectOfType<KFadeManager>();
    }

    public void InteractiveRoutine()
    {
        if (a == null)
        {
            a = StartCoroutine(InteractiveCoroutine());   
        }
    }

    private IEnumerator InteractiveCoroutine()
    {
        fadeManager.FadeOut_ImageSetActiveTrueRoutine(1);
        yield return new WaitForSeconds(3);
        fadeManager.FadeInRoutine(1);
        KTimeLineManager.Instance.StartTimeLine("12");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _other = other;
    }

    private void PlayerMoveRestroom(Collider2D other)
    {
        if (!target_Pos || other == null)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out KPlayerManager player))
            {
                player._isMoving = false;
                player.StopMoveWithTimeRoutine(0.1f, target_Pos.position);
            }
        }
    }
}
