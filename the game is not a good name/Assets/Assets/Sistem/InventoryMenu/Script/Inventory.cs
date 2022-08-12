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

    public void Addobject(InventoryInfo info)
    {
        foreach(var i in _inventoryPanelInfo.InventorysInfo)
        {
            if (i == info)
            {
                Debug.Log("they are identical");
                return;
            }
        }
        _inventoryPanelInfo.AddList(info);
        for (int i = 0; i < _listPrefab.Count; i++)
        {

            if(!_listPrefab[i].GetComponent<UISlot>().Activ)
            {
                GameObject obj = Instantiate(info.Prefab, _listPrefab[i].transform);
                obj.GetComponentInParent<UISlot>().Activ = true;
                return;
            }
        }
    }

}
