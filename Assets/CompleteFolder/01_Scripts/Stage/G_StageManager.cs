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
        public int currentStageNum = 0; // ���� Act �ѹ�
        [field: SerializeField]
        public List<string> beforeActName { get; private set; } = new List<string>();
    }

    private StageSaveData stageSaveData = new StageSaveData();

    [field:SerializeField]
    public List<GameObject> allGameObjectList { get; private set; } = new List<GameObject>();

    [field: SerializeField]
    public List<G_StageInformation> stageData { get; private set; } // �ش� ��Ʈ�� �ʿ��� ������Ʈ ������

    [SerializeField]
    private List<GameObject> clearObjects; // ��Ʈ���� ����� ������Ʈ ������
    private G_EndingManager endingManager;

    [field:SerializeField]
    public G_StageInformation currentData { get;  private set; }

    [field: SerializeField]
    public int currentStageNum; // �ӽ� Ȯ�� ��..

    [field:SerializeField]
    public GameObject[] TrueEndingItem { get; private set; } // ���� ������ �ʿ��� �����۵�

    public void Start()
    {
        stageData = GetComponent<G_StageData>().stageInformation;
        ActStart();

        foreach (string BeforeactName in stageSaveData.beforeActName)
        {
            Debug.Log(BeforeactName);
        }
    }

    // Act ����
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

    // Act ����
    public void ActStart()
    {
        SetActiveAllObject(); // ��� ������Ʈ ��Ȱ��ȭ

        JDataManager.instance.Load<StageSaveData>(out stageSaveData);
        currentStageNum = stageSaveData.currentStageNum;

        currentData = stageData[currentStageNum];

        SetActiveObject();
    }

    public void AfterSchool() // �ϱ�
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
                // ���������� �ʼ��� �ʿ��� ������Ʈ�� �̸��� �÷��� ���� ����� ������Ʈ �̸��� ������ Ȯ��
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

