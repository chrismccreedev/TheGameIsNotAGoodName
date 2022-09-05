using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PlayerTransform
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [SerializeField] private JoystickManager _joystickManager;
        [SerializeField] private CameraRotation _cameraRotation;
        [SerializeField] private float _speed;
        [SerializeField] private float _reversalSize;

        [SerializeField] private float _minRotCam;
        [SerializeField] private float _maxRotCam;

        private CharacterController _characterController;
        private float _rotationValue;

        private Vector3 _direction;
        private float _gravity = -9.81f;

        private void Start()
        {
            _characterController = GetComponentInParent<CharacterController>();
            _joystickManager.Move += Move;
            _joystickManager.Gravity += Gravity;
            _cameraRotation.Rotate += CameraRotate;
        }

        private void Gravity()
        {
            if (_characterController.isGrounded)
            {
                _direction.y = 0;
            }
            _direction.y += _gravity * Time.deltaTime;
            _characterController.Move(_direction * Time.deltaTime);
        }

        private void Move(Vector2 vector)
        {
            Vector3 move = (transform.forward * vector.y + transform.right * vector.x);
            Vector3 rotar = new Vector3(0, _camera.transform.eulerAngles.y, 0);
            transform.eulerAngles = rotar;
            _characterController.Move(move * Time.deltaTime * _speed);
        }

        private void CameraRotate(Vector2 vector2)
        {
            _camera.transform.eulerAngles += new Vector3(0, vector2.x * _reversalSize, 0);
        }
    }
}
