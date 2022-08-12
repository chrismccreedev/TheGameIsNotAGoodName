using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SistemController : MonoBehaviour
{
    [SerializeField] private SistemInfo _info;
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Menu _inventoryMenu;

    private bool _pauseValue = false;
    private bool _inventoryValue = false;

    private void Update()
    {
        if(Input.GetKeyDown(_info._pause))
        {
            _pauseValue = OpenOrCloseMenu(_pauseMenu, _pauseValue);
        }
        if(Input.GetKeyDown(_info._inventory))
        {
            _inventoryValue = OpenOrCloseMenu(_inventoryMenu, _inventoryValue);
        }
    }

    public bool OpenOrCloseMenu(Menu menu, bool value)
    {
        if (!value)
        {
            menu.Open();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            menu.Close();
            Cursor.lockState = CursorLockMode.Locked;
        }

        return !value;
    }
}
