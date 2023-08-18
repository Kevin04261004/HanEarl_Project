using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JRoofSetActiveFalse : MonoBehaviour
{
    [SerializeField] private GameObject roof;
    private void Start()
    {
        roof.SetActive(true);
    }
}
