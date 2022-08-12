using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : Menu
{
    private KeyTransform _key;
    private CameraRotation _rotation;
    private InventoryMenuUI _menuUI;

    private void Start()
    {
        _key = FindObjectOfType<KeyTransform>();
        _rotation = FindObjectOfType<CameraRotation>();
        _menuUI = GetComponent<InventoryMenuUI>();
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
        _menuUI.ClosePause();
    }
}
