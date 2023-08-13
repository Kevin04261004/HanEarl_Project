using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_DifurcationManager : MonoBehaviour
{
    private static G_DifurcationManager instance = null;

    public static G_DifurcationManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    [SerializeField]
    private G_StageManager stageManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        stageManager = FindObjectOfType<G_StageManager>().GetComponent<G_StageManager>();
    }

    public void AddInteractionObj(GameObject obj)
    {
        stageManager.AddClearObject(obj);
    }

    public void NormalEndingStart() // 액트7 이후 액트 초기화
    {
        stageManager.Act7_Reset();
    }

    public void CallEnding(string endingName) // 엔딩 호출
    {
        stageManager.EndingStart(endingName);
    }

    public bool NormalChangeEndingCheck() // 노말 엔딩이 check 되어있는지
    {
        return stageManager.NormalEndingCheck();
    }
}
