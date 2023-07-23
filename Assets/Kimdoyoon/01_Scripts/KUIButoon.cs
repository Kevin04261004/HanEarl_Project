using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    forChangeLine,
    
}

public class KUIButoon : MonoBehaviour
{
    // 사용법 : 버튼을 클릭할때 OnClicked()에서 이동할 
    private KDialogueReader kDialogueReader;
    private Button btn;
    public int num = 0;
    private void OnEnable()
    {
        kDialogueReader = FindObjectOfType<KDialogueReader>();
        btn = transform.GetComponent<Button>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyNum"> keyNum = Add할 함수가 다 다르니까 키를 하나 설정해서 사용.  </param>
    public void AddListener(ButtonState state)
    {
        btn.onClick.RemoveAllListeners();
        if(state == ButtonState.forChangeLine)
        {
            btn.onClick.AddListener(OnClicked);
        }
    }
    /* 버튼을 클릭했을때 생기는 현상. */
    public void OnClicked()
    {
        kDialogueReader.typeIndex = num - 1;
        kDialogueReader.OptionBtn_SetActive_Bool(false);
        kDialogueReader.StartReading();
    }
}
