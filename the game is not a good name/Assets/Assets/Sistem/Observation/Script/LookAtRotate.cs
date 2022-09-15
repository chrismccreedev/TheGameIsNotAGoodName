using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookAtRotate : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _minRadius;

    private float _startDrag;
    private float _endDrag;

    public event Action<int> Rotate;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDrag = eventData.position.x;
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _endDrag = eventData.position.x;

        float radius = _endDrag - _startDrag;
        if (Mathf.Abs(radius) < _minRadius)
        {
            return;
        }
        if (radius > 0)
        {
            Rotate(1);
        }
        else if (radius < 0)
        {
            Rotate(-1);
        }
    }
}
