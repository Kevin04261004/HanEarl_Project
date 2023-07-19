using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class AstarAlg : MonoBehaviour
{
    [Header("변수")]
    [Tooltip("범위")] public int size = 10;
    [Tooltip("범위 아래 왼")] public Vector2Int bottomLeft;
    [Tooltip("범위 위 오른")] public Vector2Int topRight;
    [Tooltip("시작 위치 == 좀비 위치")] public Vector2Int startPos;
    [Tooltip("타겟 위치 == 플레이어 위치")] public Vector2Int targetPos;
    [Tooltip("완성된 루트")] public List<Node> FinalNodeList;
    [Tooltip("플레이어가 범위 내에 존재하는가?")] public bool isPlayerFind;
    [Tooltip("좀비 게임 오브젝트")] public Transform Zombie;

    /* NodeArray 크기 */
    private int sizeX, sizeY;
    /* Node 이차원 배열 */
    private Node[,] NodeArray;
    /* 시작노드, 타겟노드, 현재 노드 */
    private Node StartNode, TargetNode, CurNode;
    /* 열린리스트 == 주위 노드들 추가(만약 이미 한번 거쳤던 곳이면 OpenList에서 제거) */
    private List<Node> OpenList;
    /* 닫힌리스트 <--> 열린 리스트 */
    private List<Node> ClosedList;

    private void OnEnable()
    {
        Zombie = GetComponent<Transform>();
    }

    /// <summary>
    /// 좀비가 플레이어를 찾을때 사용되는 Astar 알고리즘
    /// </summary>
    public void PathFinding(Vector2Int PlayerPos)
    {
        /* 시작 위치 == 좀비 위치 */
        startPos = new Vector2Int((int)Zombie.position.x, (int)Zombie.position.y);

        /* 인식 범위 */
        bottomLeft = new Vector2Int(startPos.x - size, startPos.y - size);
        topRight = new Vector2Int(startPos.x + size, startPos.y + size);

        /* 타겟위치 == 플레이어 위치 & 타겟 위치가 인식범위 안에 있는가? */
        targetPos = PlayerPos;
        if(targetPos.x > bottomLeft.x && targetPos.x < topRight.x && targetPos.y > bottomLeft.y && targetPos.y < topRight.y)
        {
            isPlayerFind = true;
        }
        else
        {
            isPlayerFind = false;
        }

        /* 만약 플레이어를 찾지 못했다면... 리턴 */
        if (!isPlayerFind)
        {
            return;
        }

        /* NodeArray의 크기 초기화 */
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        /* Node들의 변수(isWall, x, y) 대입 */
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Vector2 temp = new Vector2(i + bottomLeft.x, j + bottomLeft.y);
                bool isWall = false;
                Collider2D[] col = Physics2D.OverlapCircleAll(temp, 0.2f);
                foreach (var collider in col)
                {
                    if (collider.gameObject.CompareTag("Wall"))
                    {
                        isWall = true;
                    }
                }
                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
       
        /* 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화 */
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];
        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

        /* 오픈리스트가 아예 없어질 때까지 */
        while (OpenList.Count > 0)
        {
            /* 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기 */
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                {
                    CurNode = OpenList[i];
                }
            }

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            /* 타겟을 찾았을때  */
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();
                return;
            }

            /* 1은 타일 하나의 길이 */
            /* ↑ → ↓ ← 방향의 노드가 벽인가? 닫힌리스트에 존재하는가? 범위 내인가? 등등을 확인하여 OpenList에 추가 */
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
        Debug.Log("경로 못 찾음");
    }

    void OpenListAdd(int checkX, int checkY)
    {
        /* 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면 */
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 
            && checkY >= bottomLeft.y && checkY < topRight.y + 1 
            && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall 
            && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y])
            )
        {
            /* 이웃노드에 넣고, 직선은 10, 대각선은 14비용 */
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

            /* 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가 */
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }
}