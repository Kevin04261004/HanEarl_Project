using UnityEngine;

public class KEnemy : MonoBehaviour
{
    private KAstarAlg _root;
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _time = 0;
    private int _index;
    private void Awake()
    {
        _root = GetComponent<KAstarAlg>();
    }
    private void Update()
    {
        if(!_root._isPlayerFind)
        {
            return;
        }

        if(_time >= _speed)
        {
            _time = 0;
            if (_index >= _root._finalNodeList.Count - 1) return;
            _index++;
            Move(new Vector2(_root._finalNodeList[_index]._x, _root._finalNodeList[_index]._y));
        }
        else
        {
            _time += Time.deltaTime;
        }
    }
    private void Move(Vector2 targetVec)
    {
        transform.position = targetVec;
    }

    public void IndexChangeToValue(int value = 0)
    {
        _index = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        else
        {
            collision.gameObject.SetActive(false);   
        }
    }
}
