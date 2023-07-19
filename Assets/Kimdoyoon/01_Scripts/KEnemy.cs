using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEnemy : MonoBehaviour
{
    private AstarAlg root;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float time = 0;
    private int index;
    private void Awake()
    {
        root = GetComponent<AstarAlg>();
    }
    private void Update()
    {
        if(!root.isPlayerFind)
        {
            return;
        }

        if(time >= speed)
        {
            time = 0;
            if(index < root.FinalNodeList.Count-1)
            {
                index++;
                Move(new Vector2(root.FinalNodeList[index].x, root.FinalNodeList[index].y));
            }
        }
        else
        {
            time += Time.deltaTime;
        }
    }
    private void Move(Vector2 targetVec)
    {
        transform.position = targetVec;
    }
    public void FindIndex() // 플레이어가 한칸 이동하면 초기화.
    {
        index = 0;
        for (int i = 0; i < root.FinalNodeList.Count; ++i)
        {
            if((Vector2)transform.position == new Vector2(root.FinalNodeList[index].x, root.FinalNodeList[index].y))
            {
                break;
            }
        }

        return;
    }
}
