using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KNode
{
    public KNode(bool isWall, int x, int y) { _isWall = isWall; _x = x; _y = y; }

    public bool _isWall;
    public KNode _parentNode;

    /* G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H */
    public int _x, _y, _G, _H;
    public int _F => _G + _H;
}

public class KAstarAlg : MonoBehaviour
{
    [Header("변수")]
    [Tooltip("범위")] public int _size = 10;
    [Tooltip("범위 아래 왼")] public Vector2Int _bottomLeft;
    [Tooltip("범위 위 오른")] public Vector2Int _topRight;
    [Tooltip("시작 위치 == 좀비 위치")] public Vector2Int _startPos;
    [Tooltip("타겟 위치 == 플레이어 위치")] public Vector2Int _targetPos;
    [Tooltip("완성된 루트")] public List<KNode> _finalNodeList;
    [Tooltip("플레이어가 범위 내에 존재하는가?")] public bool _isPlayerFind;
    [Tooltip("좀비 게임 오브젝트")] public Transform _enemyPos;
    
    private GameObject _target;
    /* NodeArray 크기 */
    private int _sizeX, _sizeY;
    /* Node 이차원 배열 */
    private KNode[,] _nodeArray;
    /* 시작노드, 타겟노드, 현재 노드 */
    private KNode _startNode, _targetNode, _curNode;
    /* 열린리스트 == 주위 노드들 추가(만약 이미 한번 거쳤던 곳이면 OpenList에서 제거) */
    private List<KNode> _openList;
    /* 닫힌리스트 <--> 열린 리스트 */
    private List<KNode> _closedList;

    /// <summary>
    /// 좀비가 플레이어를 찾을때 사용되는 Astar 알고리즘
    /// </summary>
    public void PathFinding(Vector2Int playerPos)
    {
        
        /* 시작 위치 == 좀비 위치 */
        var position = _enemyPos.position;
        _startPos = new Vector2Int((int)position.x, (int)position.y);

        /* 인식 범위 */
        _bottomLeft = new Vector2Int(_startPos.x - _size, _startPos.y - _size);
        _topRight = new Vector2Int(_startPos.x + _size, _startPos.y + _size);

        /* 타겟위치 == 플레이어 위치 & 타겟 위치가 인식범위 안에 있는가? */
        _targetPos = playerPos;
        if (_targetPos.x > _bottomLeft.x
           && _targetPos.x < _topRight.x
           && _targetPos.y > _bottomLeft.y
           && _targetPos.y < _topRight.y)
        {
            _isPlayerFind = true;
        }
        else
        {
            _isPlayerFind = false;
        }

        /* 만약 플레이어를 찾지 못했다면... 리턴 */
        if (!_isPlayerFind)
        {
            return;
        }

        /* NodeArray의 크기 초기화 */
        _sizeX = _topRight.x - _bottomLeft.x + 1;
        _sizeY = _topRight.y - _bottomLeft.y + 1;
        _nodeArray = new KNode[_sizeX, _sizeY];

        /* Node들의 변수(isWall, x, y) 대입 */
        for (int i = 0; i < _sizeX; i++)
        {
            for (int j = 0; j < _sizeY; j++)
            {
                Vector2 temp = new Vector2(i + _bottomLeft.x, j + _bottomLeft.y);
                bool isWall = false;
                Collider2D[] col = Physics2D.OverlapCircleAll(temp, 0.2f);
                foreach (var c in col)
                {
                    if (c.gameObject.CompareTag("Wall"))
                    {
                        isWall = true;
                    }
                }
                _nodeArray[i, j] = new KNode(isWall, i + _bottomLeft.x, j + _bottomLeft.y);
            }
        }

        /* 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화 */
        _startNode = _nodeArray[_startPos.x - _bottomLeft.x, _startPos.y - _bottomLeft.y];
        _targetNode = _nodeArray[_targetPos.x - _bottomLeft.x, _targetPos.y - _bottomLeft.y];
        _openList = new List<KNode>() { _startNode };
        _closedList = new List<KNode>();
        _finalNodeList = new List<KNode>();

        /* 오픈리스트가 아예 없어질 때까지 */
        while (_openList.Count > 0)
        {
            /* 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기 */
            _curNode = _openList[0];
            for (int i = 1; i < _openList.Count; i++)
            {
                if (_openList[i]._F <= _curNode._F && _openList[i]._H < _curNode._H)
                {
                    _curNode = _openList[i];
                }
            }

            _openList.Remove(_curNode);
            _closedList.Add(_curNode);


            /* 타겟을 찾았을때 */
            if (_curNode == _targetNode)
            {
                KNode targetCurNode = _targetNode;
                while (targetCurNode != _startNode)
                {
                    _finalNodeList.Add(targetCurNode);
                    targetCurNode = targetCurNode._parentNode;
                }
                _finalNodeList.Add(_startNode);
                _finalNodeList.Reverse();
                return;
            }

            /* 1은 타일 하나의 길이 */
            /* ↑ → ↓ ← 방향의 노드가 벽인가? 닫힌리스트에 존재하는가? 범위 내인가? 등등을 확인하여 OpenList에 추가 */
            OpenListAdd(_curNode._x, _curNode._y + 1);
            OpenListAdd(_curNode._x + 1, _curNode._y);
            OpenListAdd(_curNode._x, _curNode._y - 1);
            OpenListAdd(_curNode._x - 1, _curNode._y);
        }
    }
    void OpenListAdd(int checkX, int checkY)
    {
        /* 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면 */
        if (checkX >= _bottomLeft.x && checkX < _topRight.x + 1
            && checkY >= _bottomLeft.y && checkY < _topRight.y + 1
            && !_nodeArray[checkX - _bottomLeft.x, checkY - _bottomLeft.y]._isWall
            && !_closedList.Contains(_nodeArray[checkX - _bottomLeft.x, checkY - _bottomLeft.y])
            )
        {
            /* 이웃노드에 넣고, 직선은 10, 대각선은 14비용 */
            KNode neighborNode = _nodeArray[checkX - _bottomLeft.x, checkY - _bottomLeft.y];
            int moveCost = _curNode._G + (_curNode._x - checkX == 0 || _curNode._y - checkY == 0 ? 10 : 14);

            /* 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가 */
            if (moveCost < neighborNode._G || !_openList.Contains(neighborNode))
            {
                neighborNode._G = moveCost;
                neighborNode._H = (Mathf.Abs(neighborNode._x - _targetNode._x) + Mathf.Abs(neighborNode._y - _targetNode._y)) * 10;
                neighborNode._parentNode = _curNode;

                _openList.Add(neighborNode);
            }
        }
    }
}