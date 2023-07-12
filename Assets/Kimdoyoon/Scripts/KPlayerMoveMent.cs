using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPlayerMoveMent : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Vector2 dir;
    private bool isMove;
    [SerializeField] private float gridSize;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /* key input */
        if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.UP_KEY]))
        {
            TryMove(Vector2.up);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.DOWN_KEY]))
        {
            TryMove(Vector2.down);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.LEFT_KEY]))
        {
            TryMove(Vector2.left);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.RIGHT_KEY]))
        {
            TryMove(Vector2.right);
        }
    }

    private void TryMove(Vector2 direction)
    {
        Vector2 targetPos = rigid.position + direction * gridSize;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.2f);

        bool canMove = true;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                canMove = false;
                break;
            }
        }

        if (canMove && !isMove)
        {
            StartCoroutine(MoveToTarget(targetPos));
        }
    }

    private IEnumerator MoveToTarget(Vector2 targetPosition)
    {
        isMove = true;

        while ((Vector2)transform.position != targetPosition)
        {
            Vector2 newPosition = Vector2.MoveTowards(rigid.position, targetPosition, speed * Time.fixedDeltaTime);
            rigid.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }

        isMove = false;
    }
}
