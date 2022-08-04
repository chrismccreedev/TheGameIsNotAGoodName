using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PauseMenu : MonoBehaviour
{
    private KeyTransform _key;
    private CameraRotation _rotation;
    private PauseMenuUI _menuUI;
    private SistemController _controller;

    private void Start()
    {
        _key = FindObjectOfType<KeyTransform>();
        _rotation = FindObjectOfType<CameraRotation>();
        _controller = FindObjectOfType<SistemController>();
        _menuUI = GetComponent<PauseMenuUI>();
        _controller.OnClosePause += ClosePause;
        _controller.OnOpenPause += OpenPause;
    }

    public void OpenPause()
    {
        _key.enabled = false;
        _rotation.enabled = false;
        _menuUI.OpenPause();
    }
    public void ClosePause()
    {
        _key.enabled = true;
        _rotation.enabled = true;
        _menuUI?.ClosePause();
    }
}
