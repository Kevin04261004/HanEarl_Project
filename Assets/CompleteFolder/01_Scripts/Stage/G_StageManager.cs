using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static G_StageData;

[Serializable]
public class GStageSaveData : JData
{
    public int currentStageNum = 0;

    public List<string> beforeActName = new List<string>();
}

public class G_StageManager : MonoBehaviour
{
    private GStageSaveData stageSaveData; // = new GStageSaveData();
    

    [field: SerializeField]
    public List<G_StageInformation> stageData { get; private set; }

    [SerializeField] private List<GameObject> clearObjects;
    private G_EndingManager endingManager;

    [field: SerializeField] public G_StageInformation currentData { get; private set; }
    [field: SerializeField] public int currentStageNum { get; private set; }

    private void Start()
    {
        endingManager = GetComponent<G_EndingManager>();
        stageData = GetComponent<G_StageData>().stageInformation;
        ActStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            AfterSchool();
    }

    public void AfterSchool() // Act End + InGame - > TitleScene
    {
        ActEnd(); // CS95
        KLoadingSceneManager.LoadScene("00_TitleScene");
    }

    public void ActStart()
    {

        //JDataManager.instance.Load(out stageSaveData);
        stageSaveData = JDataManager.instance.stageData;
        currentStageNum = stageSaveData.currentStageNum;

        currentData = stageData[currentStageNum];

        SetActiveObject();
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

    public void Act7_Reset()
    {
        if (endingManager.normalEndingCheck)
        {
            stageSaveData.currentStageNum = 0;
            stageSaveData.beforeActName.Clear();
            JDataManager.instance.SaveData(stageSaveData);
            KLoadingSceneManager.LoadScene("00_TitleScene");
        }
    }

    private void ActEnd()
    {
        if (TrueEndingActCheck())
        {
            stageSaveData.currentStageNum = 9;
            stageSaveData.beforeActName.Add("Act007");
        }
        else if (ObjectCheckClear() && BeforeActNameCheck())
        {
            stageSaveData.currentStageNum++;
            stageSaveData.beforeActName.Add(currentData.actName);
        }


        JDataManager.instance.SaveData(stageSaveData);
    }

    private bool TrueEndingActCheck() // 액트7 조건을 모두 클리어 하였는지
    {
        if (endingManager.normalEndingCheck && currentStageNum == 6)
        {
            return true;
        }

        return false;
    }

    private bool BeforeActNameCheck()
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
        foreach (var t in currentData.StageObject)
        {
            t.SetActive(true);
        }
    }
    private bool ObjectCheckClear()
    {
        if (stageSaveData.currentStageNum == 0)
        {
            return true;
        }

        int clearPoint = 0;

        for (int i = 0; i < currentData.interactionObj.Count; i++)
        {
            for (int j = 0; j < clearObjects.Count; j++)
            {
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

    private bool ItemCheckClear()
    {
        return false;
    }
}