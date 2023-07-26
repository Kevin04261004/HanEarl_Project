using System.Collections;
using System.Linq;
using UnityEngine;

public class KEnemy : MonoBehaviour
{
    private KAstarAlg _root;
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _time = 0;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private int _index;
    private static readonly int Dir = Animator.StringToHash("Dir");
    public GameObject _enemyPos;
    public bool _canMove = true;
    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _root = GetComponent<KAstarAlg>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        if((!_root._isPlayerFind && _root._finalNodeList.Count < _index ) || !_canMove)
        {
            return;
        }
        if(_time >= _speed)
        {
            _time = 0;
            if (_index >= _root._finalNodeList.Count - 1)
            {
                return;
            }
            _index++;
            Vector2 temp = new Vector2(_root._finalNodeList[_index]._x, _root._finalNodeList[_index]._y);
            Move(temp);
        }
        else
        {
            _time += Time.deltaTime;
        }
    }
    private void Move(Vector2 targetVec)
    {
        Vector2 v = targetVec - (Vector2)gameObject.transform.position;
        v = new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
        if (v == Vector2.down)
        {
            _animator.SetInteger(Dir, 0);
        }
        else if (v == Vector2.up)
        {
            _animator.SetInteger(Dir, 1);
        }
        else if (v == Vector2.left)
        {
            _animator.SetInteger(Dir, 2);
        }
        else if (v == Vector2.right)
        {
            _animator.SetInteger(Dir, 3);
        }
        else
        {
            ;
        }

        _enemyPos.transform.position = targetVec;
        StartCoroutine(LerpCoroutine(transform.position, _enemyPos.transform.position, _speed));

    }
    public void IndexChangeToValue()
    {
        if(!_root._isPlayerFind)
        {
            return;
        }
        _index = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        collision.gameObject.SetActive(false);
    }
    private IEnumerator LerpCoroutine(Vector3 current, Vector3 target, float time)
    {
        float elapsedTime = 0.0f;

        this.transform.position = current;
        while(elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);

            this.transform.position 
                = Vector3.Lerp(current, target, elapsedTime / time);

            yield return null;
        }

        transform.position = target;

        yield return null;
    }

    public void StopMoveWithTimeRoutine(float time, Vector3 pos)
    {
        StartCoroutine(StopMoveWithTime(time,pos));
    }
    private IEnumerator StopMoveWithTime(float time,Vector3 pos)
    {
        _canMove = false;
        yield return new WaitForSeconds(time);
        _enemyPos.transform.position = pos;
        gameObject.transform.position = pos;
        //_root.PathFinding();
        _canMove = true;
    }
}
