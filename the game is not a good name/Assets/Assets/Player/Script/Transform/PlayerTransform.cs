using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PlayerTransform
{
    public class PlayerTransform : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [SerializeField] private float _speed;
        [SerializeField] private float _reversalSize;

        [SerializeField] private float _minRotCam;
        [SerializeField] private float _maxRotCam;

        private PlayerAnumatorController _animatorController;
        private CharacterController _characterController;
        private JoystickManager _joystickManager;
        private CameraRotation _cameraRotation;
        private float _rotationValue;

        private Vector3 _direction;
        private float _gravity = -9.81f;

        private void Start()
        {
            _joystickManager = FindObjectOfType<JoystickManager>();
            _cameraRotation = FindObjectOfType<CameraRotation>();
            _characterController = GetComponentInParent<CharacterController>();
            _animatorController = GetComponent<PlayerAnumatorController>();
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
            Vector3 move;
            float rotateValue;
            float radius = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);

            if (_cameraRotation.gameObject.activeInHierarchy == false)
                move = (transform.forward * vector.y + transform.right * vector.x);
            else
                move = transform.forward * radius;
            

            if (_cameraRotation.gameObject.activeInHierarchy == false)
            {
                rotateValue = 0;
                _animatorController.MoveAnimator(vector.x, vector.y);
            }
            else if (vector.y >= 0f)
            {
                rotateValue = ((Mathf.Acos(vector.x / radius) * 180f / Mathf.PI) - 90f) * -1;
                _animatorController.MoveAnimator(Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y));
            }
            else
            {
                rotateValue = ((Mathf.Acos(vector.x / radius) * 180f / Mathf.PI) + 90f);
                _animatorController.MoveAnimator(Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y));
            }
            

            Vector3 rotar = new Vector3(0, _camera.transform.eulerAngles.y + rotateValue, 0);
            transform.eulerAngles = rotar;
            _characterController.Move(move * Time.deltaTime * _speed);
        }

        private void CameraRotate(Vector2 vector2)
        {
            _camera.transform.eulerAngles += new Vector3(0, vector2.x * _reversalSize, 0);
        }
    }
}
