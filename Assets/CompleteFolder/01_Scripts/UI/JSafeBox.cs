using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JSafeBox : MonoBehaviour
{
    public static JSafeBox Instance;
    [SerializeField] private GameObject folder;
    private JSafeBoxButton[] _nums;
    string _currentNum = "";
    public KAfterDialogue dia;
    private bool _opened = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        _nums = folder.GetComponentsInChildren<JSafeBoxButton>();
    }

    // private void Update()
    // {
    //     if (folder.activeSelf)
    //     {
    //         if (Input.GetKeyDown(KKeySetting.key_Dictionary[EKeyAction.InteractionKey]))
    //         {
    //             Debug.Log("호출됨");
    //             folder.SetActive(false);
    //             KGameManager.Instance.J_GameContinue();
    //         }
    //     }
    // }
    //
    
    
    private void GetPassword()
    {
        _currentNum = "";
        for (int i = 0; i < 4; i++)
        {
            _currentNum += _nums[i].num.ToString();
        }
        Debug.Log(_currentNum);
    }

    public void CheckPassword()
    {
        GetPassword();
        switch (_currentNum)
        {
            // 이설빈 + 주인공
            case "1331":
                SetSBUIActiveFalse();
                _opened = true;
                // 화장실열쇠, 천유현 생활기록부 떨치기였나
                dia.Used1();
                break;
            // 천유현 + 주인공
            case "뭐였더라":
                SetSBUIActiveFalse();
                _opened = true;
                dia.Used2();
                break;
            // 비번 틀림
            default:
                break;
        }
    }

    public void SetSafeBoxActive()
    {
        if (_opened)
            return;
        folder.SetActive(true);
            KGameManager.Instance.J_GamePause();
    }

    public void SetSBUIActiveFalse()
    {
        folder.SetActive(false);
            KGameManager.Instance.J_GameContinue();
    }
}