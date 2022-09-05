using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    private GameObject _enemy;
    
    public GameObject Enemy
    {
        set
        {
            _enemy = value;
        }
    }


    private void Update()
    {
        _camera.transform.LookAt(_enemy.transform);
    }
}
