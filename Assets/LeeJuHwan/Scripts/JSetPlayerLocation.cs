using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSetPlayerLocation : MonoBehaviour
{
    JPlayerData playerData = new JPlayerData();
    void Start()
    {
        //JDataManager.instance.LoadData();
        playerData = JDataManager.instance.playerData;
        if (playerData == null)
            return;
        Debug.Log(playerData.currentVec);
        transform.position = playerData.currentVec;
    }

    void Update()
    {
        // ¿˙¿Â
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerData.currentVec = transform.position;
            JDataManager.instance.SaveData(playerData);
        }
    }
}
