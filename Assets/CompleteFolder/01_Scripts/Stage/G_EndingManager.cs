using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_EndingManager : MonoBehaviour
{
    public Dictionary<string, bool> endingList;

    public void CallEnding(string endingName) // stage매니저 에게서 정보를 받고 엔딩을 호출함
    {
        endingList.Add(endingName, true);
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
