using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerTransform
{
    public class JoystickManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _joystickFon;
        [SerializeField] private GameObject _joystick;

        private PlayerAnumatorController _controller;

        private float _radius;
        private bool _triggerMove = false;
        private Canvas _canvas;
        private Vector2 _startPositionJoystickFon;
        private Vector2 _startPositionJoystick;

        public event Action<Vector2> Move;
        public event Action Gravity;

        private void Start()
        {
            _radius = ((_joystickFon.GetComponent<RectTransform>().rect.width/2f)/22f)*18.5f;
            _controller = FindObjectOfType<PlayerAnumatorController>();
            Debug.Log(_radius);
            _startPositionJoystickFon = _joystickFon.transform.localPosition;
            _startPositionJoystick = _joystick.transform.localPosition;
            _canvas = GetComponentInParent<Canvas>();
        }
        private void FixedUpdate()
        {
            if (_triggerMove)
            {
                Move(_joystick.transform.localPosition / _radius);
            }
            Gravity();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _controller.StartMove();
            _joystickFon.transform.position = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickFon.transform.localPosition = _startPositionJoystickFon;
            _controller.EndMove();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _triggerMove = true;
            _joystick.transform.localPosition = JoystickMath(eventData.position) / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _triggerMove = false;
            _joystick.transform.localPosition = _startPositionJoystick;
        }

        private Vector2 JoystickMath(Vector2 value)
        {
            Vector2 zeroVector = value - (Vector2)_joystickFon.transform.position;
            float distans = Mathf.Sqrt(zeroVector.x * zeroVector.x + zeroVector.y * zeroVector.y);
            if(distans <= _radius * _canvas.scaleFactor)
            {
                return zeroVector;
            }
            else
            {
                float cos = zeroVector.x / distans;
                float sin = zeroVector.y / distans;
                float x = _radius * _canvas.scaleFactor * cos;
                float y = _radius * _canvas.scaleFactor * sin;
                return new Vector2(x, y);
            }
        }    
    }
}
