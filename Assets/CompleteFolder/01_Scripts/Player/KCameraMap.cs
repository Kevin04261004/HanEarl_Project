using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KCameraMap : MonoBehaviour
{
    //[SerializeField] private Vector2 _center;
    //[SerializeField] private Vector2 _size;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _cameraSpeed;
    private Vector3 _offset;
    private float _height;
    private float _width;
    
    private void Awake()
    {
        _offset = new Vector3(0, 0, -10);
    }
    private void Start()
    {
        if (Camera.main != null)
        {
            _height = Camera.main.orthographicSize;
        }
        _width = _height * Screen.width / Screen.height;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(_center, _size);
    //}
    private void LateUpdate()
    {
        var position1 = _target.transform.position;
        gameObject.transform.position = Vector3.Lerp(position1 + _offset, position1 + _offset, Time.deltaTime * _cameraSpeed);

        //float lx = _size.x * 0.5f - _width;
        //var position = transform.position;
        //float clampX = Mathf.Clamp(position.x, _center.x - lx, _center.x + lx);

        //float ly = _size.y * 0.5f - _height;
        //float clampY = Mathf.Clamp(position.y, _center.y - ly, _center.y + ly);
        //position = new Vector3(clampX, clampY, -10);
        //transform.position = position;

    }
}
