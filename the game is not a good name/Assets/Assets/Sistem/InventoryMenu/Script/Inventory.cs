using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryInfo _inventoryPanelInfo;
    [SerializeField] private Transform _parents;

    private List<GameObject> _listPrefab = new();

    public event Action<int> ReturnAmount;
    private void Start()
    {
        CreateSlot();
    }

    public void CreateSlot()
    {
        for(int i = _listPrefab.Count; i < _inventoryPanelInfo.NumPanel; i++)
        {
            _listPrefab.Add(Instantiate(_inventoryPanelInfo.PrefabPanel, _parents));
        }
    }

    public void AddItem(ItemInfo info, int num, int amount)
    {
        int i = 0;
        do
        {
            GameObject obj = Instantiate(info.Prefab, _listPrefab[num + i].transform);
            obj.GetComponent<UIItem>().Item = info;
            if(amount <= obj.GetComponent<UIItem>().Item.MaxAmount)
            {
                obj.GetComponent<UIItem>().Amount = amount;
                amount = 0;
            }
            else if(amount > obj.GetComponent<UIItem>().Item.MaxAmount)
            {
                obj.GetComponent<UIItem>().Amount = obj.GetComponent<UIItem>().Item.MaxAmount;
                amount -= obj.GetComponent<UIItem>().Item.MaxAmount;
            }
            obj.GetComponentInParent<UISlot>().UIItem = obj.GetComponent<UIItem>();
            i++;
        }
        while (amount != 0);
        ReturnAmount(0);
    }

    public bool AddAmountItem(ItemInfo info, int amount)
    {
        UIItem[] UIItems = FindObjectsOfType<UIItem>();
        for(int i = 0; i < UIItems.Length; i++)
        {
            if (UIItems[i].Item == info)
            {
                if(UIItems[i].Amount + amount <= UIItems[i].Item.MaxAmount)
                {
                    UIItems[i].Amount += amount;
                    ReturnAmount(0);
                    return true;
                }
                else if (UIItems[i].Amount + amount > UIItems[i].Item.MaxAmount)
                {
                    int m = UIItems[i].Item.MaxAmount - UIItems[i].Amount;
                    UIItems[i].Amount = UIItems[i].Item.MaxAmount;
                    amount -= m;
                    ReturnAmount(amount);
                }
            }
        }
        return false;
    }

}
