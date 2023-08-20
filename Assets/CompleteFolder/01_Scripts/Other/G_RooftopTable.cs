using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_RooftopTable : MonoBehaviour
{
    [SerializeField] private KFadeManager fadeManager;
    [SerializeField] private GameObject b_obj;
    [SerializeField] private Transform target_Pos;
    [SerializeField] private Collider2D _other;

    private void Awake()
    {
        fadeManager = FindObjectOfType<KFadeManager>();
    }

    public void InteractiveRoutine()
    {
        StartCoroutine(InteractiveCoroutine());
    }

    private IEnumerator InteractiveCoroutine()
    {
        //if (TryGetComponent(out JItem jitem))
        //{
        //    jitem.Get();
        //}
        yield return new WaitForSeconds(2);
        b_obj.SetActive(true);
        yield return new WaitForSeconds(1);
        fadeManager.FadeOut_ImageSetActiveTrueRoutine(0.1f);
        b_obj.SetActive(false);
        yield return new WaitForSeconds(3);
        PlayerMoveRestroom(_other);
        fadeManager.FadeInRoutine(3);
        this.gameObject.SetActive(false);
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
