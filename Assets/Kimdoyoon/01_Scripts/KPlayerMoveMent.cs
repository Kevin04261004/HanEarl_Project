using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPlayerMoveMent : MonoBehaviour
{
    [SerializeField] private float curSpeed;
    
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float gridSize;
    private bool isMoving;
    private bool isRunning;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        curSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            curSpeed = runSpeed;
        }
        if(!KGameManager.instance.canInput)
        {
            return;
        }
        /* key input */
        if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.UP_KEY]))
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("direction", 1);
            TryMove(Vector2.up);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.DOWN_KEY]))
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("direction", 0);
            TryMove(Vector2.down);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.LEFT_KEY]))
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("direction", 2);
            TryMove(Vector2.left);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[KKeyAction.RIGHT_KEY]))
        {
            spriteRenderer.flipX = true;
            animator.SetInteger("direction", 3);
            TryMove(Vector2.right);
        }
        if (Input.GetKeyUp(KKeySetting.key_Dictionary[KKeyAction.UP_KEY]))
        {
            animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[KKeyAction.DOWN_KEY]))
        {
            animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[KKeyAction.LEFT_KEY]))
        {
            animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[KKeyAction.RIGHT_KEY]))
        {
            animator.SetBool("isWalking", false);
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

        if (canMove && !isMoving)
        {
            StartCoroutine(MoveToTarget(targetPos));
        }
    }

    private IEnumerator MoveToTarget(Vector2 targetPosition)
    {
        isMoving = true;
        animator.SetBool("isWalking", true);
        while ((Vector2)transform.position != targetPosition)
        {
            Vector2 newPosition = Vector2.MoveTowards(rigid.position, targetPosition, curSpeed * Time.fixedDeltaTime);
            rigid.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }
        isMoving = false;
    }
}
