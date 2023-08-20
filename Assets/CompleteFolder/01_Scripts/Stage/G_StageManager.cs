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
    public bool normalEndingCheck = false;
    public bool trueEndingCheck = false;
    public List<string> playedActName = new List<string>();
}

public class G_StageManager : MonoBehaviour
{
    [SerializeField]
    private List<JItem> wantItems;
    private GStageSaveData stageSaveData; // = new GStageSaveData();

    [field: SerializeField]
    public List<G_StageInformation> stageData { get; private set; }

    [SerializeField] private List<GameObject> clearObjects;
    private G_EndingManager endingManager;

    [field: SerializeField] public G_StageInformation currentData { get; private set; }
    [field: SerializeField] public int currentStageNum { get; private set; }

    private void Awake()
    {
        endingManager = GetComponent<G_EndingManager>();
        stageData = GetComponent<G_StageData>().stageInformation;
    }

    private void Start()
    {
        ActStart();
    }

    public bool NormalEndingCheck()
    {
        return stageSaveData.normalEndingCheck;
    }

    public bool TrueEndingCheck()
    {
        return stageSaveData.trueEndingCheck;
    }

    public void AfterSchool() // Act End + InGame - > TitleScene
    {
        ActEnd(); // CS95
        KLoadingSceneManager.LoadScene("00_TitleScene");
    }

    public void ActStart()
    {
        stageSaveData = JDataManager.instance.stageData;
        currentStageNum = stageSaveData.currentStageNum;

        currentData = stageData[currentStageNum];

        SetActiveObject();

        // currentStagenum = 4 => act4.1
        //                   5 => act 5

        if (currentStageNum >= 3)
        {
            G_InventorySystem.Instance.J_AddItem(wantItems[0]);
        }
        if (currentStageNum >= 4)
        {
            G_InventorySystem.Instance.J_AddItem(wantItems[1]);
        }
        if (currentStageNum >= 5)
        {
            G_InventorySystem.Instance.J_AddItem(wantItems[2]);
            G_InventorySystem.Instance.J_AddItem(wantItems[4]);
            G_InventorySystem.Instance.J_AddItem(wantItems[5]);
            G_InventorySystem.Instance.J_AddItem(wantItems[6]);
        }
        if(currentStageNum >= 6)
        {
            G_InventorySystem.Instance.J_AddItem(wantItems[3]);

        }

        endingManager.NormalAndTrueEndingTextObjSetting();

        foreach (string actName in stageSaveData.beforeActName)
        {
            endingManager.Endinginitialization(actName);
        }
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

        if (NormalEndingCheck() && stageSaveData.currentStageNum == 0)
        {
            return;
        }
        else
        {
            ActEnd();
        }
    }

    public void Act7_Reset()
    {
        if (endingManager.normalEndingCheck)
        {
            stageSaveData.currentStageNum = 0;
            stageSaveData.beforeActName.Clear();
            stageSaveData.normalEndingCheck = true;
            JDataManager.instance.SaveData(stageSaveData);
        }
    }

    public void Act9_TrueEndingCheck()
    {
        if (endingManager.trueEndingCheck)
        {
            stageSaveData.trueEndingCheck = true;
            JDataManager.instance.SaveData(stageSaveData);
        }
    }

    private void ActEnd()
    {
        if (TrueEndingActCheck())
        {
            stageSaveData.currentStageNum = 9;
            stageSaveData.beforeActName.Add("Act007");
        }
        else if (ObjectCheckClear() && BeforeActNameCheck() && currentStageNum != 7)
        {
            stageSaveData.currentStageNum++;
            stageSaveData.beforeActName.Add(currentData.actName);
        }
        /* 도윤 추가 */
        bool hasAct = false;
        foreach (var act in stageSaveData.playedActName)
        {
            if (act == currentData.actName)
            {
                hasAct = true;
            }
        }

        if (!hasAct)
        {
            stageSaveData.playedActName.Add(currentData.actName);
        }

        /* 여기까지 도윤 */
        JDataManager.instance.SaveData(stageSaveData);
    }

    private bool TrueEndingActCheck() // 액트7 조건을 모두 클리어 하였는지
    {
        if (stageSaveData.normalEndingCheck && currentStageNum == 6)
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
        if (currentData.stageInteractionSkip)
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
}