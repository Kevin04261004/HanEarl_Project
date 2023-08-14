using UnityEngine;

[System.Serializable]
public class KDialogue
{
    [Tooltip("인덱스")] public int index;
    [Tooltip("캐릭터이름")] public string character_Name;
    [Tooltip("캐릭터 이미지")] public string characterImage_Name;
    [Tooltip("선택지")] public bool hasOption;
    [Tooltip("대사 내용")] public string[] contexts;
    [Tooltip("선택지 내용")] public string[] option_Contexts;
    [Tooltip("버튼 클릭시 몇 줄로 이동")] public string[] nextLine;
}
[System.Serializable]
public class KDialogueEvent
{
    public string eventName;

    public Vector2 line;
    public Vector2 N_line;
    public KDialogue[] dialogues;
}