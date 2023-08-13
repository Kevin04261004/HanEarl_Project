using UnityEngine;

public class KAfterDialogue : MonoBehaviour
{
    [SerializeField] private GameObject BeforeObj;
    [SerializeField] private GameObject NewObj;
    [SerializeField] private GameObject BeforeObj2;
    [SerializeField] private GameObject NewObj2;
    public void Used()
    {
        if (BeforeObj != null)
        {
            BeforeObj.SetActive(false);
        }
        if (NewObj != null)
        {
            NewObj.SetActive(true);
        }
        if (BeforeObj2 != null)
        {
            BeforeObj2.SetActive(false);
        }
        if (NewObj2 != null)
        {
            NewObj2.SetActive(true);
        }
    }

    public void GetItem(string itemName)
    {
        
    }
}
