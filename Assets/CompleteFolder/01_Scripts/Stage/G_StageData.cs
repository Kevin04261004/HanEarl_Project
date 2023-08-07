using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_StageData : MonoBehaviour
{
    [System.Serializable]
    public class G_StageInformation
    {
        [field: SerializeField]
        public List<GameObject> StageObject { get; private set; } = new List<GameObject>(); // ���������� �����ϴ� ��ü object
        [field: SerializeField]
        public List<KInteractiveObject> interactionObj { get; private set; } = new List<KInteractiveObject>(); // �������� Ŭ��� �ʼ�
        [field: SerializeField]
        public string actName { get; private set; }
        [field: SerializeField]
        public string[] clearBeforeActName { get; private set; }
        [field: SerializeField]
        public Dictionary<bool, GameObject> activeObjects { get; private set; } = new Dictionary<bool, GameObject>();
    }

    [field : SerializeField]
    public List<G_StageInformation> stageInformation { get; private set; } = new List<G_StageInformation>();
}


