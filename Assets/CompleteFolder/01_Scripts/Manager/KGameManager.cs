using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGameManager : MonoBehaviour
{
    public static KGameManager Instance = null;
    public bool _canInput = true;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;   
        }
        else
        {
            Destroy(this.gameObject);
        }
        Time.timeScale = 1.0f;
    }
    public void GamePause()
    {
        Time.timeScale = 0.0f;
        _canInput = false;
    }
    public void GameContinue()
    {
        Time.timeScale = 1.0f;
        _canInput = true;
    }
}
