using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEnemy : MonoBehaviour
{
    private AstarAlg root;
    public bool playerNoneMove;
    [SerializeField] private float speed = 0.2f;
    private Coroutine move_Coroutine;
    public void OnEnable()
    {
        root = GetComponent<AstarAlg>();
    }
    public void MoveStart()    // AstarAlg���� ��Ʈ�� ã������ MoveStop()�� ����
    {
        playerNoneMove = true;
        move_Coroutine = StartCoroutine(MoveNext());
    }
    public void MoveStop()
    {
        playerNoneMove = false;
        if(move_Coroutine != null)
        {
            StopCoroutine(move_Coroutine);
        }
    }
    public IEnumerator MoveNext()
    {
        int index = 0;
        while(playerNoneMove)
        {
            if (!root.isPlayerFind)
            {
                yield break;
            }
            if (index +1 < root.FinalNodeList.Count)
            {
                index++;
            }
            else
            {
                yield break;
            }
            gameObject.transform.position = new Vector2(root.FinalNodeList[index].x, root.FinalNodeList[index].y);
            yield return new WaitForSeconds(speed);
        }
    }

}
