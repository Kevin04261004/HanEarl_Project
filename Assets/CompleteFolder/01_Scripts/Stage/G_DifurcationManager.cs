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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddInteractionObj(GameObject obj)
    {
        stageManager.AddClearObject(obj);
    }

    public void CallEnding(string endingName) // 엔딩 호출
    {
        stageManager.EndingStart(endingName);
    }
}
