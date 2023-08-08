using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


using static G_StageData;
using UnityEngine.SceneManagement;

public class G_StageManager : MonoBehaviour
{
    [SerializeField]
    protected class StageSaveData : JData
    {
        [field: SerializeField]
        public int currentStageNum = 0; // 현재 Act 넘버
        [field: SerializeField]
        public List<string> beforeActName { get; private set; } = new List<string>();
    }

    private StageSaveData stageSaveData = new StageSaveData();

    [field:SerializeField]
    public List<GameObject> allGameObjectList { get; private set; } = new List<GameObject>();

    [field: SerializeField]
    public List<G_StageInformation> stageData { get; private set; } // 해당 액트에 필요한 오브젝트 데이터

    [SerializeField]
    private List<GameObject> clearObjects; // 액트에서 사용한 오브젝트 데이터
    private G_EndingManager endingManager;

    [field:SerializeField]
    public G_StageInformation currentData { get;  private set; }

    [field: SerializeField]
    public int currentStageNum; // 임시 확인 용..

    [field:SerializeField]
    public GameObject[] TrueEndingItem { get; private set; } // 최종 엔딩에 필요한 아이템들

    public void Start()
    {
        stageData = GetComponent<G_StageData>().stageInformation;
        ActStart();

        foreach (string BeforeactName in stageSaveData.beforeActName)
        {
            Debug.Log(BeforeactName);
        }
    }

    // Act 종료
    public void ActEnd()
    {

        if (ObjectCheckClear() && BeforeActNameCheck())
        {
            stageSaveData.currentStageNum++;
            stageSaveData.beforeActName.Add(currentData.actName);
            SceneManager.LoadScene("Test_Title");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        JDataManager.instance.SaveData<StageSaveData>(stageSaveData);
        ActStart();
    }

    // Act 시작
    public void ActStart()
    {
        SetActiveAllObject(); // 모든 오브젝트 비활성화

        JDataManager.instance.Load<StageSaveData>(out stageSaveData);
        currentStageNum = stageSaveData.currentStageNum;

        currentData = stageData[currentStageNum];

        SetActiveObject();
    }

    public void AfterSchool() // 하교
    {
        ActEnd();
        SceneManager.LoadScene("JMainMenu");
    }

    public void AddClearObject(GameObject obj)
    {
        clearObjects.Add(obj);

        clearObjects = clearObjects.Distinct().ToList();
    }

    public void EndingStart(string endingName)
    {
        stageSaveData.beforeActName.Add(endingName);
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
            if (stageSaveData.beforeActName.Contains(actName))
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

    private void SetActiveAllObject()
    {
        for(int i = 0;i < allGameObjectList.Count; i++)
        {
            allGameObjectList[i].SetActive(false);
        }
    }

    private bool ObjectCheckClear()
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
                        return true;
                    }
                }
            }

        }

        clearObjects.Clear();
        return false;
    }
}

