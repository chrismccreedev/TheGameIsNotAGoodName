using UnityEngine;
using Sirenix.OdinInspector;

public class MenuController : MonoBehaviour
{
    [SerializeField, ReadOnly] private Menu _menu;

    private MenuController[] _menus;
    private bool _pauseValue = false;

    private void Start()
    {
        _menu = GetComponent<Menu>();
        _menus = FindObjectsOfType<MenuController>();
    }
    private void Update()
    {
        if(_pauseValue && (Input.GetKeyDown(_menu.Key()) || Input.GetKeyDown(KeyCode.Escape)))
        {
            _menu.Close();
            foreach(var menu in _menus)
            {
                menu.enabled = true;
            }
            _pauseValue = !_pauseValue;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(!_pauseValue && Input.GetKeyDown(_menu.Key()))
        {
            foreach (var menu in _menus)
            {
                menu.enabled = false;
            }
            enabled = true;
            _pauseValue = !_pauseValue;
            Cursor.lockState = CursorLockMode.None;
            _menu.Open();
        }
    }
}
