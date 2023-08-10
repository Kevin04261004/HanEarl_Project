using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndingType
{
    None,
    BedEndingA,
    BedEndingB, 
    BedEndingC,
    BedEndingD,
    BedEndingE,
    BedEndingF,
    NormalEnding,
    TrueEnding
}

public class G_EndingManager : MonoBehaviour
{
    public EndingType currentEndingName;
    public Dictionary<string, bool> endingList;

    [field:SerializeField] public bool normalEndingCheck { get; private set; } = false;

    public void CallEnding(string endingName) // stage매니저 에게서 정보를 받고 엔딩을 호출함
    {
        EndingTypeSetting(endingName);
    }

    private void EndingTypeSetting(string endingName)
    {
        currentEndingName = (EndingType)System.Enum.Parse(typeof(EndingType), endingName);

        switch (currentEndingName)
        {
            case EndingType.BedEndingA:
                endingList.Add(endingName, true);
                break;
            case EndingType.BedEndingB:
                endingList.Add(endingName, true);
                break;
            case EndingType.BedEndingC:
                endingList.Add(endingName, true);
                break;
            case EndingType.BedEndingD:
                endingList.Add(endingName, true);
                break;
            case EndingType.BedEndingE:
                endingList.Add(endingName, true);
                break;
            case EndingType.BedEndingF:
                endingList.Add(endingName, true);
                break;
            case EndingType.NormalEnding:
                endingList.Add(endingName, true);
                normalEndingCheck = true;
                G_DifurcationManager.Instance.NormalEndingStart();
                break;
            case EndingType.TrueEnding:
                endingList.Add(endingName, true);
                break;
            default:
                break;
        }
    }

    public bool CheckEndingList(string endingName)
    {
        foreach (KeyValuePair<string, bool> check in endingList)
        {
            if (check.Key == endingName && check.Value)
            {
                return true;
            }
        }

        return false;
    }
}
