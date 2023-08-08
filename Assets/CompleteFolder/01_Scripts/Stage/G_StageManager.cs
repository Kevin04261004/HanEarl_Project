using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static G_StageData;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class GStageSaveData : JData
{
    public int currentStageNum = 0;

    //public List<string> beforeActName { get; private set; } = new List<string>(); 
    public List<string> beforeActName = new List<string>();
}

public class G_StageManager : MonoBehaviour
{
    private GStageSaveData stageSaveData; // = new GStageSaveData();

    [field: SerializeField] public List<GameObject> allGameObjectList { get; private set; } = new List<GameObject>();

    [field: SerializeField]
    public List<G_StageInformation> stageData { get; private set; }

    [SerializeField] private List<GameObject> clearObjects;
    private G_EndingManager endingManager;

    [field: SerializeField] public G_StageInformation currentData { get; private set; }

    [field: SerializeField] public int currentStageNum; // �ӽ� Ȯ�� ��..

    [field: SerializeField] public GameObject[] TrueEndingItem { get; private set; }

    public void Start()
    {
        stageData = GetComponent<G_StageData>().stageInformation;
        ActStart();

        foreach (string BeforeactName in stageSaveData.beforeActName)
        {
            Debug.Log(BeforeactName);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            AfterSchool();
    }

    private void ActEnd()
    {
        if (ObjectCheckClear() && BeforeActNameCheck())
        {
            stageSaveData.currentStageNum++;
            stageSaveData.beforeActName.Add(currentData.actName);
        }
        JDataManager.instance.SaveData(stageSaveData);
    }

    public void ActStart()
    {
        SetActiveAllObject();

        //JDataManager.instance.Load(out stageSaveData);
        stageSaveData = JDataManager.instance.stageData;
        currentStageNum = stageSaveData.currentStageNum;


        currentData = stageData[currentStageNum];

        SetActiveObject();
    }

    public void AfterSchool() // Act End + InGame - > TitleScene
    {
        ActEnd();
        SceneManager.LoadScene("00_TitleScene");
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

    private bool BeforeActNameCheck() // ������ Ŭ������ ��Ʈ�� �̸��� üũ��.
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
        for (int i = 0; i < allGameObjectList.Count; i++)
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