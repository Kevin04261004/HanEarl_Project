using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KUITitleSceneManager : MonoBehaviour
{
    [SerializeField] private Image _name_Input_Image;
    [SerializeField] private List<Sprite> _endingSprites;
    [SerializeField] private InputField wantName;
    [SerializeField] private Image backGround;
    
    private void Start()
    {
        switch (JDataManager.instance.stageData.currentStageNum)
        {
            case 7:
                backGround.sprite = _endingSprites[1];
                break;
            case 8:
                backGround.sprite = _endingSprites[2];
                break;
            case 9:
                backGround.sprite = _endingSprites[3];
                break;
            default:
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

    public void OnClick_ChangeTo_GameScene()
    {
        KLoadingSceneManager.LoadScene("01_GameScene");
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