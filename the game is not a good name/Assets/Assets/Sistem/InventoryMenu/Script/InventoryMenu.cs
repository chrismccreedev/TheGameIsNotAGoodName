using UnityEngine;

public class InventoryMenu : Menu
{
    private InventoryMenuUI _menuUI;
    protected override void Start()
    {
        base.Start();
        _menuUI = GetComponent<InventoryMenuUI>();
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
        return _info._inventory;
    }
}
