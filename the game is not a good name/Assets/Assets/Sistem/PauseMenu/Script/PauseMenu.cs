using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PauseMenu : Menu
{
    private PauseMenuUI _menuUI;
    protected override void Start()
    {
        base.Start();
        _menuUI = GetComponent<PauseMenuUI>();
    }

    public override void Open()
    {
        _rotation.enabled = false;
        _menuUI.OpenPause();
    }
    public override void Close()
    {
        _rotation.enabled = true;
        _menuUI.ClosePause();
    }
    public override KeyCode Key()
    {
        return _info._pause;
    }
}
