using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGameManager : MonoBehaviour
{
    public static KGameManager instance = null;
    public bool canInput = true;
    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
    }
    public void GamePause()
    {
        Time.timeScale = 0.0f;
        canInput = false;
    }
    public void GameContinue()
    {
        Time.timeScale = 1.0f;
        canInput = true;
    }
}
