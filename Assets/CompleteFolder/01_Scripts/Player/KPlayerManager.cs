using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EDied
{
    DiedFromEnemy01,
    Died02,
    
}

public class KPlayerManager : MonoBehaviour
{
    [SerializeField] private float _curSpeed;
    [SerializeField] private Transform _playerPos;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 _dir;
    [SerializeField] private float _gridSize;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private Animator _animator;
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private List<KAstarAlg> _activeEnemy;
    [field: SerializeField] public int _inputKey { get; private set; } = -1;
    public bool _isMoving;
    private static readonly int Direction = Animator.StringToHash("direction");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.DownKey]) && (_inputKey == 0 || _inputKey == -1))
        {
            _inputKey = 0;
            _animator.SetInteger(Direction, 0);
            TryMove(Vector2.down);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.UpKey]) && (_inputKey == 1 || _inputKey == -1))
        {
            _inputKey = 1;
            _animator.SetInteger(Direction, 1);
            TryMove(Vector2.up);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.LeftKey]) && (_inputKey == 2 || _inputKey == -1))
        {
            _inputKey = 2;
            _animator.SetInteger(Direction, 2);
            TryMove(Vector2.left);
        }
        else if (Input.GetKey(KKeySetting.key_Dictionary[EKeyAction.RightKey]) && (_inputKey == 3 || _inputKey == -1))
        {
            _inputKey = 3;
            _animator.SetInteger(Direction, 3);
            TryMove(Vector2.right);
        }
        
        if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.DownKey]))
        {
            if (_animator.GetInteger(Direction) != 0)
            {
                return;
            }
            ResetInputKey();
            _animator.SetBool(IsWalking, false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.UpKey]))
        {
            if (_animator.GetInteger(Direction) != 1)
            {
                return;
            }
            ResetInputKey();
            _animator.SetBool(IsWalking, false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.LeftKey]))
        {
            if (_animator.GetInteger(Direction) != 2)
            {
                return;
            }
            ResetInputKey();
            _animator.SetBool(IsWalking, false);
        }
        else if (Input.GetKeyUp(KKeySetting.key_Dictionary[EKeyAction.RightKey]))
        {
            if (_animator.GetInteger(Direction) != 3)
            {
                return;
            }

            ResetInputKey();
            _animator.SetBool(IsWalking, false);
        }
        /* Interaction key input */
        if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.InteractionKey]))
        {
            var targetPos = new Vector2(0,0);
            switch (_animator.GetInteger(Direction))
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
            var collidersW = Physics2D.OverlapCircleAll(_rigidbody2D.position, 0.2f);
            foreach (var c in collidersW)
            {
                if (!c.CompareTag("InteractiveObject")) continue;
                c.TryGetComponent(out KInteractiveObject interactiveObject);
                interactiveObject.Interactive();
                if (c.transform.childCount < 1) continue;
                if (c.transform.GetChild(0).TryGetComponent(out Kmark markObj))
                {
                    c.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            var colliders = Physics2D.OverlapCircleAll(targetPos, 0.2f);
            foreach (var c in colliders)
            {
                if (!c.CompareTag("InteractiveObject")) continue;
                c.TryGetComponent(out KInteractiveObject interactiveObject);
                interactiveObject.Interactive();
                if (c.transform.childCount < 1) continue;
                if (c.transform.GetChild(0).TryGetComponent(out Kmark markObj))
                {
                    c.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }


    private void TryMove(Vector2 direction)
    {
        Vector2 targetPos = _rigidbody2D.position + direction * _gridSize;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.4f);

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
        if (_enemyParent.gameObject.activeSelf)
        {
            AllEnemyTarget(targetPosition);
        }
        _isMoving = true;
        _animator.SetBool(IsWalking, true);
        while ((Vector2)transform.position != targetPosition && _isMoving)
        {
            Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, targetPosition, _curSpeed * Time.fixedDeltaTime);
            _playerPos.position = targetPosition;
            _rigidbody2D.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }
        _isMoving = false;
    }
    
    private void AllEnemyTarget(Vector2 targetPosition)
    {
        // Enemy가 타겟을 플레이어로 잡고 A*알고리즘 돌리기
        _activeEnemy.Clear();
        for (int i = 0; i < _enemyParent.childCount; ++i)
        {
            if (!_enemyParent.GetChild(i).gameObject.activeSelf)
            {
                continue;
            }
            if (_enemyParent.GetChild(i).TryGetComponent(out KAstarAlg astar))
            {
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

    public void ResetInputKey(int dir = -1)
    {
        _inputKey = dir;
    }
    public void Set_Player_Dir(int dir = 0)
    {
        _animator.SetInteger(Direction, dir);
    }

    public void Died(EDied eDied)
    {
        switch (eDied)
        {
            case EDied.DiedFromEnemy01:
                KTimeLineManager.Instance.StartTimeLine("03");
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case EDied.Died02:
                break;
            default:
                break;
        }
    }


    public void StopMoveWithTimeRoutine(float time, Vector3 pos)
    {
        StartCoroutine(StopMoveWithTime(time,pos));
    }
    private IEnumerator StopMoveWithTime(float time,Vector3 pos)
    {
        _playerPos.transform.position = pos;
        gameObject.transform.position = pos;
        yield return new WaitForSeconds(time);
    }
}