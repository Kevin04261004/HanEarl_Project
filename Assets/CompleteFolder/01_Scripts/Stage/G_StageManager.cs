using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static G_StageData;

public class G_StageManager : MonoBehaviour
{
    public List<G_StageInformation> stageData; // 해당 액트에 필요한 오브젝트 데이터
    [SerializeField]
    private List<GameObject> clearObjects; // 액트에서 사용한 오브젝트 데이터
    [field:SerializeField]
    public List<string> beforeActName { get; private set; } = new List<string>();

    private G_EndingManager endingManager;

    [field:SerializeField]
    public G_StageInformation currentData { get;  private set; }

    int currentStageNum = 0; // 현재 Act 넘버
    [field:SerializeField] 
    public bool actClear { get; private set; }

    [field:SerializeField]
    public GameObject[] TrueEndingItem { get; private set; } // 최종 엔딩에 필요한 아이템들

    public void Start()
    {
        ActStart();
    }

    // Act 종료
    public void ActEnd()
    {
        ObjectCheckClear();

        if (actClear && BeforeActNameCheck())
        {
            currentStageNum++;
            beforeActName.Add(currentData.actName);
            Debug.Log("액트 종료, 뉴 액트로 넘어갑니다.");
        }
        else
        {
            Debug.Log("액트 다시 시작");
        }

        ActStart();
    }

    // Act 시작
    public void ActStart()
    {
        actClear = false;
        currentData = stageData[currentStageNum];
        SetActiveObject();
    }

    public void AddClearObject(GameObject obj)
    {
        clearObjects.Add(obj);

        clearObjects = clearObjects.Distinct().ToList();
    }

    public int StageNumber()
    {
        return currentStageNum;
    }

    public void EndingStart(string endingName)
    {
        beforeActName.Add(endingName);
        endingManager.CallEnding(endingName);
    }

    private bool BeforeActNameCheck() // 이전에 클리어한 액트의 이름을 체크함.
    {
        if (currentData.clearBeforeActName == null)
        {
            return true;
        }

        int clearPoint = 0;

        foreach (string actName in currentData.clearBeforeActName) 
        {
            if (beforeActName.Contains(actName))
            {
                clearPoint++;
            }
        }

        if (clearPoint == currentData.clearBeforeActName.Length)
        {
            return true;
        }

        return false;
    }

    private void SetActiveObject()
    {
        for (int i = 0; i < currentData.StageObject.Count; i++)
        {
            currentData.StageObject[i].SetActive(true);
        }
    }

    private void ObjectCheckClear()
    {
        int clearPoint = 0;

        for (int i = 0; i < currentData.interactionObj.Count; i++)
        {
            for (int j = 0; j < clearObjects.Count; j++)
            {
                // 스테이지에 필수로 필요한 오브젝트의 이름과 플레이 도중 사용한 오브젝트 이름을 대조해 확인
                if (currentData.interactionObj[i].gameObject.name == clearObjects[j].name)
                {
                    clearPoint++;
                    if (clearPoint == currentData.interactionObj.Count)
                    {
                        actClear = true;
                    }
                }
            }

        }

        clearObjects.Clear();
    }
}

