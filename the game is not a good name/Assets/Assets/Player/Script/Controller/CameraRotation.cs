using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CameraRotation : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector2 _startPosition;

    public event Action<Vector2> Rotate;

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
