using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JSafeBox : MonoBehaviour
{
    public static JSafeBox instance;
    [SerializeField] 
    private GameObject folder;
    private JSafeBoxButton[] nums;
    string currentNum = "";
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
        nums = transform.gameObject.GetComponentsInChildren<JSafeBoxButton>();
    }

    //
    //public string GetPassword()
    //{
    //    currentNum = "";
    //    for (int i = 0; i < 4; i++)
    //    {
    //        currentNum += nums[i].num.ToString();
    //    }
    //    Debug.Log(currentNum);
    //    return currentNum;
    //}

    public void CheckPassword()
    {
        switch(currentNum)
        {
            case "1331":
                gameObject.SetActive(false);
                // 화장실열쇠, 천유현 생활기록부 떨치기였나
                break;
            case "뭐였더라":
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void SetSafeBoxActive()
    {
        folder.SetActive(!folder.activeSelf);
        if(folder.activeSelf)
        {
            KGameManager.Instance.J_GamePause();
        }
        else
        {
            KGameManager.Instance.J_GameContinue();
        }
    }
}
