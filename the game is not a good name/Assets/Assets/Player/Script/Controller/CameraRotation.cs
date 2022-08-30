using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CameraRotation : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector2 _oldPosition;
    private Vector2 _startPosition;

    public event Action<Vector2> Rotate;

    /*
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        _oldPosition = transform.position;
    }
    private void Update()
    {
        _newPosition = Input.mousePosition;
        //Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        _oldPosition = _newPosition;
    }
    */

    public void OnDrag(PointerEventData eventData)
    {
        Rotate(eventData.position - _startPosition);
        _startPosition = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = eventData.position;
    }
}
