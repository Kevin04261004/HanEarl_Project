using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KUITitleSceneManager : MonoBehaviour
{
    [SerializeField] private Image _name_Input_Image;
    [SerializeField] private List<Sprite> _endingSprites;
    [SerializeField] private InputField wantName;
    [SerializeField] private Image backGround;

    private void Awake()
    {
    }

    private void Start()
    {
        switch (JDataManager.instance.stageData.currentStageNum)
        {
            case 1:
                backGround.sprite = _endingSprites[1];
                break;
            case 2:
                backGround.sprite = _endingSprites[2];
                break;
            case 3:
                backGround.sprite = _endingSprites[3];
                break;
        }
        
    }

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

    public void OnClick_GameStartExit_Btn()
    {
        _name_Input_Image.gameObject.SetActive(false);
    }

    // 이 함수는 이름 정하기 완료 버튼, 이름 정한 후면 그냥 게임 시작버튼에서 실행 됨
    public void OnClick_ChangeTo_GameScene()
    {
        //SceneManager.LoadScene("01_GameScene"); 본겜 사용할 코드
        SceneManager.LoadScene("01_G_GameScene"); // 임시 테스트용
    }

    public void OnClick_SetName_Btn()
    {
        if (wantName.text == "")
            return;
        JPlayerData playerdata = JDataManager.instance.playerData;
        playerdata.name = wantName.text;
        JDataManager.instance.SaveData(playerdata);
        OnClick_ChangeTo_GameScene();
    }
}