using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KPlayerManager : MonoBehaviour
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
    private KDialogueReader dialogueReader;
    [SerializeField] private Transform enemys;
    [SerializeField] private List<AstarAlg> activeEnemy;
    private void Awake()
    {
        dialogueReader = GetComponent<KDialogueReader>();
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
        /* move key input */
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
        /* Interaction key input */
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[KKeyAction.INTERACTION_KEY]))
        {
            Vector2 targetPos = new Vector2(0,0);
            switch (animator.GetInteger("direction"))
            {
                case 0:
                    targetPos = rigid.position + Vector2.down * gridSize;
                    break;
                case 1:
                    targetPos = rigid.position + Vector2.up * gridSize;
                    break;
                case 2:
                    targetPos = rigid.position + Vector2.left * gridSize;
                    break;
                case 3:
                    targetPos = rigid.position + Vector2.right * gridSize;
                    break;
                default:
                    break;
            }
            Collider2D[] collidersW = Physics2D.OverlapCircleAll(rigid.position, 0.2f);
            foreach (Collider2D collider in collidersW)
            {
                if (collider.CompareTag("InteractiveObject"))
                {
                    collider.GetComponent<KInteractiveObject>().Interactive();
                }

            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("InteractiveObject"))
                {
                    collider.GetComponent<KInteractiveObject>().Interactive();
                }
                
            }
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
        AllEnemyTarget(targetPosition);
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

    public void AllEnemyTarget(Vector2 targetPosition)
    {
        if(!enemys)
        {
            Debug.Log("enemys 오브젝트 없음");
            return;
        }
        // 모든 Enemy들에게 캐릭터가 이동한 위치를 target으로 이동하게 만들기.
        activeEnemy.Clear();
        for (int i = 0; i < enemys.childCount; ++i)
        {
            if (enemys.GetChild(i).gameObject.activeSelf)
            {
                activeEnemy.Add(enemys.GetChild(i).gameObject.GetComponent<AstarAlg>());
            }
        }
        if(activeEnemy.Count == 0)
        {
            return;
        }
        for (int i = 0; i < activeEnemy.Count; ++i)
        {
            activeEnemy[i].PathFinding(new Vector2Int((int)targetPosition.x, (int)targetPosition.y));
            activeEnemy[i].transform.GetComponent<KEnemy>().FindIndex();
        }
    }
}
