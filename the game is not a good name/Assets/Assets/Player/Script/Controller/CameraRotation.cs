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
        Cursor.lockState = CursorLockMode.Locked;
        _oldPosition = transform.position;
    }
    private void Update()
    {
        _newPosition = Input.mousePosition;
        Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        _oldPosition = _newPosition;
    }
}
