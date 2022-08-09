using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PauseMenu : Menu
{
    private KeyTransform _key;
    private CameraRotation _rotation;
    private PauseMenuUI _menuUI;

    private void Start()
    {
        _key = FindObjectOfType<KeyTransform>();
        _rotation = FindObjectOfType<CameraRotation>();
        _menuUI = GetComponent<PauseMenuUI>();
    }

    public override void Open()
    {
        _key.enabled = false;
        _rotation.enabled = false;
        _menuUI.OpenPause();
    }
    public override void Close()
    {
        _key.enabled = true;
        _rotation.enabled = true;
        _menuUI?.ClosePause();
    }
}