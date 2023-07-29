using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGameManager : MonoBehaviour
{
    public static KGameManager Instance = null;
    public bool _canInput = true;
    private KPlayerManager _playerManager;
    private Animator _playerAnimator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

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
    public void GamePause()
    {
        Time.timeScale = 0.0f;
        _canInput = false;
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
