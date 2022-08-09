using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void OpenPause()
    {
        _panel.SetActive(true);
    }
    public void ClosePause()
    {
        _panel.SetActive(false);
    }
}
