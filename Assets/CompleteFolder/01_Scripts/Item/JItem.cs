using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JItem : MonoBehaviour
{
    public KDialogue[] dialogues_01;
    public JItemInstance itemData;

    private void Start()
    {
        dialogues_01 = GetComponent<KInteractiveObject>().GetDialogue();
    }

    public void Get()
    {
        itemData.isGet = true;
        itemData.isInvn = true;
        gameObject.SetActive(false);
        G_InventorySystem.Instance.J_AddItem(itemData);
    }
}
