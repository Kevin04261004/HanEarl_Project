using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KUITitleSceneManager : MonoBehaviour
{
    [SerializeField] private Image _name_Input_Image;
    public void OnClick_GameStart_Btn()
    {
        _name_Input_Image.gameObject.SetActive(true);
    }
    public void OnClick_GameStartExit_Btn()
    {
        _name_Input_Image.gameObject.SetActive(false);
    }
    public void OnClick_ChangeTo_GameScene()
    {
        SceneManager.LoadScene("01_GameScene");
    }
}