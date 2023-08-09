using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFairTaleBook_State
{
    First,
    Second,
    Third,
    
}

public class KFairTaleBook : MonoBehaviour
{
    [SerializeField] private EFairTaleBook_State _state;
    [SerializeField] private GameObject B_ForAct2;
    [SerializeField] private GameObject B_ForAct2_After_GetBook;
    public void Used()
    {
        B_ForAct2.SetActive(false);
        B_ForAct2_After_GetBook.SetActive(true);
    }
}
