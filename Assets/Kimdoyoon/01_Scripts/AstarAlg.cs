using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class AstarAlg : MonoBehaviour
{
    [Header("����")]
    [Tooltip("����")] public int size = 10;
    [Tooltip("���� �Ʒ� ��")] public Vector2Int bottomLeft;
    [Tooltip("���� �� ����")] public Vector2Int topRight;
    [Tooltip("���� ��ġ == ���� ��ġ")] public Vector2Int startPos;
    [Tooltip("Ÿ�� ��ġ == �÷��̾� ��ġ")] public Vector2Int targetPos;
    [Tooltip("�ϼ��� ��Ʈ")] public List<Node> FinalNodeList;
    [Tooltip("�÷��̾ ���� ���� �����ϴ°�?")] public bool isPlayerFind;
    [Tooltip("���� ���� ������Ʈ")] public Transform Zombie;

    /* NodeArray ũ�� */
    private int sizeX, sizeY;
    /* Node ������ �迭 */
    private Node[,] NodeArray;
    /* ���۳��, Ÿ�ٳ��, ���� ��� */
    private Node StartNode, TargetNode, CurNode;
    /* ��������Ʈ == ���� ���� �߰�(���� �̹� �ѹ� ���ƴ� ���̸� OpenList���� ����) */
    private List<Node> OpenList;
    /* ��������Ʈ <--> ���� ����Ʈ */
    private List<Node> ClosedList;

    private void OnEnable()
    {
        Zombie = GetComponent<Transform>();
    }

    /// <summary>
    /// ���� �÷��̾ ã���� ���Ǵ� Astar �˰���
    /// </summary>
    public void PathFinding(Vector2Int PlayerPos)
    {
        /* ���� ��ġ == ���� ��ġ */
        startPos = new Vector2Int((int)Zombie.position.x, (int)Zombie.position.y);

        /* �ν� ���� */
        bottomLeft = new Vector2Int(startPos.x - size, startPos.y - size);
        topRight = new Vector2Int(startPos.x + size, startPos.y + size);

        /* Ÿ����ġ == �÷��̾� ��ġ & Ÿ�� ��ġ�� �νĹ��� �ȿ� �ִ°�? */
        targetPos = PlayerPos;
        if(targetPos.x > bottomLeft.x && targetPos.x < topRight.x && targetPos.y > bottomLeft.y && targetPos.y < topRight.y)
        {
            isPlayerFind = true;
        }
        else
        {
            isPlayerFind = false;
        }

        /* ���� �÷��̾ ã�� ���ߴٸ�... ���� */
        if (!isPlayerFind)
        {
            return;
        }

        /* NodeArray�� ũ�� �ʱ�ȭ */
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        /* Node���� ����(isWall, x, y) ���� */
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
       
        /* ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ */
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];
        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

        /* ���¸���Ʈ�� �ƿ� ������ ������ */
        while (OpenList.Count > 0)
        {
            /* ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű�� */
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


            /* Ÿ���� ã������  */
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

            /* 1�� Ÿ�� �ϳ��� ���� */
            /* �� �� �� �� ������ ��尡 ���ΰ�? ��������Ʈ�� �����ϴ°�? ���� ���ΰ�? ����� Ȯ���Ͽ� OpenList�� �߰� */
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
        Debug.Log("��� �� ã��");
    }

    void OpenListAdd(int checkX, int checkY)
    {
        /* �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ� */
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 
            && checkY >= bottomLeft.y && checkY < topRight.y + 1 
            && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall 
            && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y])
            )
        {
            /* �̿���忡 �ְ�, ������ 10, �밢���� 14��� */
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

            /* �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰� */
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