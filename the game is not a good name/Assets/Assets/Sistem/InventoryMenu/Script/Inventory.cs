using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryPanelInfo _inventoryPanelInfo;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parents;

    private List<GameObject> _listPrefab = new();

    private void Start()
    {
        CreateSlot();
    }

    public void CreateSlot()
    {
        for(int i = _listPrefab.Count; i < _inventoryPanelInfo.NumPanel; i++)
        {
            _listPrefab.Add(Instantiate(_prefab, _parents));
        }
    }

}
