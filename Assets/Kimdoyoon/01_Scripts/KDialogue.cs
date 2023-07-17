using UnityEngine;

[System.Serializable]
public class KDialogue
{
    [Tooltip("캐릭터이름")] public string character_Name;
    [Tooltip("캐릭터 이미지")] public string characterImage_Name;
    [Tooltip("선택지")] public bool hasOption;
    [Tooltip("대사 내용")] public string[] contexts;
    [Tooltip("선택지 내용")] public string[] option_Contexts;
}
[System.Serializable]
public class KDialogueEvent
{
    public string eventName;

    public Vector2 line;
    public KDialogue[] dialogues;
}