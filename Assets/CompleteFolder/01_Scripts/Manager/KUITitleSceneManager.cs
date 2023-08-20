using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KUITitleSceneManager : MonoBehaviour
{
    [SerializeField] private Image _name_Input_Image;
    [SerializeField] private Image _exit_Window;
    [SerializeField] private List<Sprite> _endingSprites;
    [SerializeField] private InputField wantName;
    [SerializeField] private Image backGround;
    [SerializeField] private List<Text> _texts;
    [SerializeField] private Button _startGame_Btn;
    [SerializeField] private Button _newGame_Btn;
    [SerializeField] private Button _exit_Btn;
    
    private void Start()
    {
        if (JDataManager.instance.stageData.normalEndingCheck)
        {
            backGround.sprite = _endingSprites[2];
        }
        else
        {
            switch (JDataManager.instance.stageData.currentStageNum)
            {
                case 5 or 6 or 7:
                    backGround.sprite = _endingSprites[1];
                    foreach (Text tx in _texts)
                    {
                        tx.color = Color.white;
                    }
                    break;
                default:
                    backGround.sprite = _endingSprites[0];
                    break;
            }
        }
        if (JDataManager.instance.stageData.trueEndingCheck)
        {
            _startGame_Btn.gameObject.SetActive(false);
            
            bool hasEnding = false;
            foreach (var act in JDataManager.instance.stageData.beforeActName)
            {
                if (act == "TrueEnding")
                {
                    hasEnding = true;
                }
            }

            if (hasEnding)
            {
                backGround.sprite = _endingSprites[3];
            }
            else
            {
                backGround.sprite = _endingSprites[0];
            }
        }
    }

    // NewGame 버튼에 쓰임
    public void OnClick_NewGame_Btn()
    {
        _name_Input_Image.gameObject.SetActive(true);
    }

    //GameStart 버튼에 쓰임
    public void OnClick_GameStart_Btn()
    {
        // 대충 이름 있는지(액트 1도 시작 안했는지) 확인하는 코드
        if (JDataManager.instance.playerData.name == "")
        {
            _name_Input_Image.gameObject.SetActive(true);
        }
        else
        {
            OnClick_ChangeTo_GameScene();
        }
    }

    public void OnClick_GameExitOn_Btn()
    {
        _exit_Window.gameObject.SetActive(true);
    }

    public void OnClick_GameExitOff_Btn()
    {
        _exit_Window.gameObject.SetActive(false);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void OnClick_NewGameExit_Btn()
    {
        _name_Input_Image.gameObject.SetActive(false);
    }

    public void OnClick_ChangeTo_GameScene()
    {
        KLoadingSceneManager.LoadScene("01_GameScene");
    }

    public void OnClick_ResetGame_Btn()
    {
        JDataManager.instance.stageData.currentStageNum = 0;
        JDataManager.instance.stageData.beforeActName.Clear();
        JDataManager.instance.stageData.normalEndingCheck = false;
        JDataManager.instance.stageData.trueEndingCheck = false;
        JDataManager.instance.stageData.playedActName.Clear();
        JPlayerData playerdata = JDataManager.instance.playerData;
        playerdata.name = "";
        JDataManager.instance.SaveData(JDataManager.instance.stageData);
        JDataManager.instance.SaveData(playerdata);

        OnClick_GameStart_Btn();
    }

    // 이름 변경창 '확인' 버튼에 쓰임
    public void OnClick_SetName_Btn()
    {
        if (wantName.text == "")
            return;
        JDataManager.instance.stageData.currentStageNum = 0;
        JDataManager.instance.stageData.beforeActName.Clear();
        JDataManager.instance.stageData.normalEndingCheck = false;
        JDataManager.instance.stageData.trueEndingCheck = false;
        JDataManager.instance.stageData.playedActName.Clear();
        JPlayerData playerdata = JDataManager.instance.playerData;
        playerdata.name = wantName.text;
        JDataManager.instance.SaveData(JDataManager.instance.stageData);
        JDataManager.instance.SaveData(playerdata);
        OnClick_ChangeTo_GameScene();
    }
}