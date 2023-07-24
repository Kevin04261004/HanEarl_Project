using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KPlayerManager : MonoBehaviour
{
    [SerializeField] private float _curSpeed;
    
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 _dir;
    [SerializeField] private float _gridSize;
    private bool _isMoving;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private KDialogueReader _dialogueReader;
    [SerializeField] private Transform _enemys;
    [SerializeField] private List<KAstarAlg> _activeEnemy;
    private void Awake()
    {
        _dialogueReader = GetComponent<KDialogueReader>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _curSpeed = _walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _curSpeed = _runSpeed;
        }
        if(!KGameManager.Instance._canInput)
        {
            return;
        }
        /* move key input */
        if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.UpKey]))
        {
            _spriteRenderer.flipX = false;
            _animator.SetInteger("direction", 1);
            TryMove(Vector2.up);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.DownKey]))
        {
            _spriteRenderer.flipX = false;
            _animator.SetInteger("direction", 0);
            TryMove(Vector2.down);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.LeftKey]))
        {
            _spriteRenderer.flipX = false;
            _animator.SetInteger("direction", 2);
            TryMove(Vector2.left);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.RightKey]))
        {
            _spriteRenderer.flipX = true;
            _animator.SetInteger("direction", 3);
            TryMove(Vector2.right);
        }
        if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.UpKey]))
        {
            _animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.DownKey]))
        {
            _animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.LeftKey]))
        {
            _animator.SetBool("isWalking", false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.RightKey]))
        {
            _animator.SetBool("isWalking", false);
        }
        /* Interaction key input */
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.InteractionKey]))
        {
            Vector2 targetPos = new Vector2(0,0);
            switch (_animator.GetInteger("direction"))
            {
                case 0:
                    targetPos = _rigidbody2D.position + Vector2.down * _gridSize;
                    break;
                case 1:
                    targetPos = _rigidbody2D.position + Vector2.up * _gridSize;
                    break;
                case 2:
                    targetPos = _rigidbody2D.position + Vector2.left * _gridSize;
                    break;
                case 3:
                    targetPos = _rigidbody2D.position + Vector2.right * _gridSize;
                    break;
                default:
                    break;
            }
            Collider2D[] collidersW = Physics2D.OverlapCircleAll(_rigidbody2D.position, 0.2f);
            foreach (var c in collidersW)
            {
                if (!c.CompareTag("InteractiveObject")) continue;
                c.TryGetComponent(out KInteractiveObject interactiveObject);
                interactiveObject.Interactive();

            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.2f);
            foreach (var c in colliders)
            {
                if (!c.CompareTag("InteractiveObject")) continue;
                c.TryGetComponent(out KInteractiveObject interactiveObject);
                interactiveObject.Interactive();
            }
        }
    }


    private void TryMove(Vector2 direction)
    {
        Vector2 targetPos = _rigidbody2D.position + direction * _gridSize;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.2f);

        bool canMove = true;
        foreach (var c in colliders)
        {
            if (c.CompareTag("Wall"))
            {
                canMove = false;
                break;
            }
        }

        if (canMove && !_isMoving)
        {
            StartCoroutine(MoveToTarget(targetPos));
        }
    }
    private IEnumerator MoveToTarget(Vector2 targetPosition)
    {
        if (_enemys.gameObject.activeSelf)
        {
            AllEnemyTarget(targetPosition);   
        }
        _isMoving = true;
        _animator.SetBool("isWalking", true);
        while ((Vector2)transform.position != targetPosition)
        {
            Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, targetPosition, _curSpeed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }
        _isMoving = false;
    }
    
    private void AllEnemyTarget(Vector2 targetPosition)
    {
        // Enemy가 타겟을 플레이어로 잡고 A*알고리즘 돌리기
        _activeEnemy.Clear();
        for (int i = 0; i < _enemys.childCount; ++i)
        {
            if (_enemys.GetChild(i).gameObject.activeSelf)
            {
                _enemys.GetChild(i).TryGetComponent(out KAstarAlg astar);
                _activeEnemy.Add(astar);
            }
        }
        if(_activeEnemy.Count == 0)
        {
            return;
        }
        for (int i = 0; i < _activeEnemy.Count; ++i)
        {
            _activeEnemy[i].PathFinding(new Vector2Int((int)targetPosition.x, (int)targetPosition.y));
            _activeEnemy[i].transform.TryGetComponent(out KEnemy enemy);
            enemy.IndexChangeToValue();
        }
    }
}