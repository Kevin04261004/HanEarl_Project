using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KGameManager : MonoBehaviour
{
    public static KGameManager Instance = null;
    public bool _canInput = true;
    public bool _jcanInput = true;
    private KPlayerManager _playerManager;
    private Animator _playerAnimator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    [SerializeField] private bool _isGameEnd;
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
        _playerManager = FindObjectOfType<KPlayerManager>();
        _playerAnimator = _playerManager.GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (_isGameEnd)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("01_GameScene");   
            }
        }
    }

    public void IsGameEndTrue()
    {
        _isGameEnd = true;
    }
    public void GamePause()
    {
        Time.timeScale = 0.0f;
        _canInput = false;
        _playerAnimator.SetBool(IsWalking, false);
        _playerManager.ResetInputKey();
    }
    
    public void J_GamePause()
    {
        _canInput = false;
        _jcanInput = false;
        _playerAnimator.SetBool(IsWalking, false);
        _playerManager.ResetInputKey();
    }
    
    public void J_GameContinue()
    {
        _canInput = true;
        _jcanInput = true;
        _playerAnimator.SetBool(IsWalking, false);
        _playerManager.ResetInputKey();
    }
    
    public void GameContinue()
    {
        Time.timeScale = 1.0f;
        _canInput = true;
        _playerAnimator.SetBool(IsWalking, false);
        _playerManager.ResetInputKey();
    }
    public void SetCanInput(bool isTrue)
    {
        _canInput = isTrue;
    }
}
