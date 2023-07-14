using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KUITitleSceneManager : MonoBehaviour
{
    public void OnClick_GameStart_Btn()
    {
        SceneManager.LoadScene("KGameScene");
    }
}
