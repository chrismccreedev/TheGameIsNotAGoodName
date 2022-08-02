using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraRotation : MonoBehaviour
{
    private Vector2 _oldPosition;
    private Vector2 _newPosition;

    public event Action<Vector2> Rotate;

    private void Start()
    {
        _oldPosition = transform.position;
    }
    private void Update()
    {
        _newPosition = Input.mousePosition;
        Rotate(_newPosition - _oldPosition);
        _oldPosition = _newPosition;
    }
}
