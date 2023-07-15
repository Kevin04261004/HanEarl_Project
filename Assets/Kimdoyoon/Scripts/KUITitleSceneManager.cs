using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KUITitleSceneManager : MonoBehaviour
{
    [SerializeField] private Image StartGameBG_Image;
    public void OnClick_GameStart_Btn()
    {
        StartGameBG_Image.gameObject.SetActive(true);
    }
    public void OnClick_GameStartExit_Btn()
    {
        StartGameBG_Image.gameObject.SetActive(false);
    }
    public void OnClick_ChangeTo_GameScene()
    {
        SceneManager.LoadScene("KTitleScene");
    }
}
