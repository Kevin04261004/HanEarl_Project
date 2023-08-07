using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static G_StageData;

public class G_StageManager : MonoBehaviour
{
    public List<G_StageInformation> stageData; // �ش� ��Ʈ�� �ʿ��� ������Ʈ ������
    [SerializeField]
    private List<GameObject> clearObjects; // ��Ʈ���� ����� ������Ʈ ������
    [field:SerializeField]
    public List<string> beforeActName { get; private set; } = new List<string>();

    private G_EndingManager endingManager;

    [field:SerializeField]
    public G_StageInformation currentData { get;  private set; }

    int currentStageNum = 0; // ���� Act �ѹ�
    [field:SerializeField] 
    public bool actClear { get; private set; }

    [field:SerializeField]
    public GameObject[] TrueEndingItem { get; private set; } // ���� ������ �ʿ��� �����۵�

    public void Start()
    {
        ActStart();
    }

    // Act ����
    public void ActEnd()
    {
        ObjectCheckClear();

        if (actClear && BeforeActNameCheck())
        {
            currentStageNum++;
            beforeActName.Add(currentData.actName);
            Debug.Log("��Ʈ ����, �� ��Ʈ�� �Ѿ�ϴ�.");
        }
        else
        {
            Debug.Log("��Ʈ �ٽ� ����");
        }

        ActStart();
    }

    // Act ����
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

    private bool BeforeActNameCheck() // ������ Ŭ������ ��Ʈ�� �̸��� üũ��.
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
                // ���������� �ʼ��� �ʿ��� ������Ʈ�� �̸��� �÷��� ���� ����� ������Ʈ �̸��� ������ Ȯ��
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

