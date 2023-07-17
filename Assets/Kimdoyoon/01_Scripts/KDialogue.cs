using UnityEngine;

[System.Serializable]
public class KDialogue
{
    [Tooltip("ĳ�����̸�")] public string character_Name;
    [Tooltip("ĳ���� �̹���")] public string characterImage_Name;
    [Tooltip("������")] public bool hasOption;
    [Tooltip("��� ����")] public string[] contexts;
    [Tooltip("������ ����")] public string[] option_Contexts;
}
[System.Serializable]
public class KDialogueEvent
{
    public string eventName;

    public Vector2 line;
    public KDialogue[] dialogues;
}