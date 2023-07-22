using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* 카메라에 넣어주세요. */
public class KObjectFade : MonoBehaviour
{
    private Camera player_Camera;
    [SerializeField] private GameObject target;
    private float fadeTime;
    [SerializeField] private TilemapCollider2D curCollider;
    private void Awake()
    {
        player_Camera = GetComponent<Camera>();
        target = FindObjectOfType<KPlayerManager>().gameObject;
    }

    private void Update()
    {
        print(0);
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(gameObject.transform.position, Vector3.forward, 100);
        if (hit.Length != 0)
        {
            for (int i = 0; i < hit.Length; ++i)
            {
                if (hit[i].transform.CompareTag("FadeObj"))
                {
                    hit[i].transform.GetComponent<Tilemap>().color = Color.red;
                }
            }
        }

    }
}
