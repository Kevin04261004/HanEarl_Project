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
    // ���� : ��ư�� Ŭ���Ҷ� OnClicked()���� �̵��� 
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
    /// <param name="keyNum"> keyNum = Add�� �Լ��� �� �ٸ��ϱ� Ű�� �ϳ� �����ؼ� ���.  </param>
    public void AddListener(ButtonState state)
    {
        btn.onClick.RemoveAllListeners();
        if(state == ButtonState.forChangeLine)
        {
            btn.onClick.AddListener(OnClicked);
        }
    }
    /* ��ư�� Ŭ�������� ����� ����. */
    public void OnClicked()
    {
        kDialogueReader.typeIndex = num - 1;
        kDialogueReader.OptionBtn_SetActive_Bool(false);
        kDialogueReader.StartReading();
    }
}
