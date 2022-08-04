using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private KeyTransform _keyTransform;
    [SerializeField] private CameraRotation _cameraRotation;
    [SerializeField] private float _speed;

    [SerializeField] private float _minRotCam;
    [SerializeField] private float _maxRotCam;

    private CharacterController _characterController;
    private float _rotationValue;


    private void Start()
    {
        _characterController = GetComponentInParent<CharacterController>();
        _keyTransform.KeyDown += Move;
        _cameraRotation.Rotate += CameraRotate;
    }

    private void Move(int forwardValue, int sideValue)
    {
        Vector3 move = transform.forward * forwardValue + transform.right * sideValue;
        Vector3 rotar = _camera.transform.eulerAngles;
        transform.eulerAngles = rotar;
        _characterController.Move(move * 0.03f * _speed);
    }

    private void CameraRotate(Vector2 vector2)
    {
        _camera.transform.eulerAngles += new Vector3(0, vector2.x * 0.5f, 0);
    }    
}
