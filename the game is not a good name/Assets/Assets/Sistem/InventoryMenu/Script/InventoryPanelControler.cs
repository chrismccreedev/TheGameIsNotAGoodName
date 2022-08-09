using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InventoryPanelControler : MonoBehaviour
{
    [SerializeField] private InventoryPanelInfo _inventoryPanelInfo;
    [SerializeField] private Transform _parents;

    [SerializeField, ReadOnly] private List<GameObject> _panels;

    [Button]
    public void CreatePanel()
    {
        DestroyPanel();
        for(int i = 0; i < _inventoryPanelInfo.NumPanel; i++)
        {
            _panels.Add(Instantiate(_inventoryPanelInfo.PrefabPanel, _parents));
        }
    }
    public void DestroyPanel()
    {
        foreach (GameObject panel in _panels)
        {
            Destroy(panel);
        }
        _panels = new List<GameObject>();
    }
}
