using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFairTaleBook_State
{
    Act_Second,
    Act_Third,
}

public class KFairTaleBook : MonoBehaviour
{
    [SerializeField] private EFairTaleBook_State _state;
    [SerializeField] private GameObject B_ForAct2;
    [SerializeField] private GameObject B_ForAct2_After_GetBook;
    [SerializeField] private GameObject C_ForAct3;
    [SerializeField] private GameObject C_ForAct3_After_GetBook;
    
    public void Used()
    {
        switch (_state)
        {
            case EFairTaleBook_State.Act_Second:
                B_ForAct2.SetActive(false);
                B_ForAct2_After_GetBook.SetActive(true);
                break;
            case EFairTaleBook_State.Act_Third:
                C_ForAct3.SetActive(false);
                C_ForAct3_After_GetBook.SetActive(true);
                break;
            default:
                break;
        }
    }
}
