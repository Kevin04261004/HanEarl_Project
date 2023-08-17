using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public enum EndingType
{
    None,
    BadEndingA,
    BadEndingB, 
    BadEndingC,
    BadEndingD,
    BadEndingE,
    BadEndingF,
    NormalEnding,
    TrueEnding
}

public class G_EndingManager : MonoBehaviour
{
    public EndingType currentEndingName;
    public Dictionary<string, bool> endingList = new Dictionary<string, bool>();
    [field:SerializeField] public bool normalEndingCheck { get; private set; } = false;
    [field: SerializeField] public bool trueEndingCheck { get; private set; } = false;

    [SerializeField] private GameObject[] endingTextObj;
    [SerializeField] private GameObject normalEndingText;
    [SerializeField] private GameObject trueEndingText;

    public void CallEnding(string endingName) // stage매니저 에게서 정보를 받고 엔딩을 호출함
    {
        EndingTypeSetting(endingName);
    }

    public void Endinginitialization(string endingName)
    {
        EndingClearSetting(endingName);
    }

    private void EndingClearSetting(string endingName)
    {
        for (int i = 0; i < endingTextObj.Length; i++)
        {
            if (endingTextObj[i].name == endingName)
            {
                endingTextObj[i].SetActive(true);
            }
        }

        if (G_DifurcationManager.Instance.NormalChangeEndingCheck())
        {
            normalEndingText.SetActive(true);
        }

        if (G_DifurcationManager.Instance.TrueChangeEndingCheck())
        {
            trueEndingText.SetActive(true);
        }
    }

    private void EndingTypeSetting(string endingName)
    {
        currentEndingName = (EndingType)System.Enum.Parse(typeof(EndingType), endingName);

        switch (currentEndingName)
        {
            case EndingType.BadEndingA:
                endingList.Add(endingName, true);
                break;
            case EndingType.BadEndingB:
                endingList.Add(endingName, true);
                break;
            case EndingType.BadEndingC:
                endingList.Add(endingName, true);
                break;
            case EndingType.BadEndingD:
                endingList.Add(endingName, true);
                break;
            case EndingType.BadEndingE:
                endingList.Add(endingName, true);
                break;
            case EndingType.BadEndingF:
                endingList.Add(endingName, true);
                break;
            case EndingType.NormalEnding:
                endingList.Add(endingName, true);
                normalEndingCheck = true;
                G_DifurcationManager.Instance.NormalEndingStart();
                break;
            case EndingType.TrueEnding:
                endingList.Add(endingName, true);
                trueEndingCheck = true;
                G_DifurcationManager.Instance.TrueEndingStart();
                break;
            default:
                break;
        }
    }
}
