using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCameraMap : MonoBehaviour
{
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 size;
    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSpeed;

    private Vector3 offset;
    private float height;
    private float width;
    
    private void Awake()
    {
        offset = transform.position - target.transform.position;
    }
    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    private void LateUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(target.transform.position, target.transform.position + offset, Time.deltaTime * cameraSpeed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);
        transform.position = new Vector3(clampX, clampY, -10);

    }
}
