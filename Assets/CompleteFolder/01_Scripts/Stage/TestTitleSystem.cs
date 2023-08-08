using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTitleSystem : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("01_G_GameScene");
    }
}
