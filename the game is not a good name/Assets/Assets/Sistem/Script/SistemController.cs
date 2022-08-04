using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SistemController : MonoBehaviour
{
    [SerializeField] private SistemInfo _info;

    public event Action OnOpenPause;
    public event Action OnClosePause;

    private void Update()
    {
        if(Input.GetKeyDown(_info._pause))
        {
            if(!_info.PauseValue)
            {
                OnOpenPause();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                OnClosePause();
                Cursor.lockState = CursorLockMode.Locked;
            }
            _info.Pause();
        }
    }
}
