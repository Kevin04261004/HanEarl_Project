using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_StageData : MonoBehaviour
{
    [System.Serializable]
    public class G_StageInformation
    {
        [field:SerializeField]
        public List<GameObject> StageObject { get; private set; } = new List<GameObject>(); // 스테이지에 존재하는 전체 object

        [field: SerializeField]
        public List<KInteractiveObject> interactionObj { get; private set; } = new List<KInteractiveObject>(); // 스테이지 클리어에 필수

        [field:SerializeField]
        public List<JItem> needItem { get; private set; } = new List<JItem> ();

        [field:SerializeField] public string actName { get; private set; }

        [field:SerializeField] public string[] clearBeforeActName { get; private set; }
    }

    [field : SerializeField]
    public List<G_StageInformation> stageInformation { get; private set; } = new List<G_StageInformation>();
}


