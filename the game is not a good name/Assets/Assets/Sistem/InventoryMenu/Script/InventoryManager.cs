using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventoryInfo _inventoryPanelInfo;
        [SerializeField] private Transform _parents;

        private List<GameObject> _listPrefab = new();
        private SaveManager _saveManager = new SaveManager();

        public event Action<int> ReturnAmount;
        private void Start()
        {
            CreateSlot();
            LoadData();
        }

        public void CreateSlot()
        {
            for (int i = _listPrefab.Count; i < _inventoryPanelInfo.NumPanel; i++)
            {
                _listPrefab.Add(Instantiate(_inventoryPanelInfo.PrefabPanel, _parents));
            }
        }

        public void AddItem(DefaultItemInfo info, int num, int amount)
        {
            int i = 0;
            do
            {
                if (i + num >= _listPrefab.Count)
                {
                    ReturnAmount(amount);
                    return;
                }
                while (_listPrefab[i + num].GetComponent<SlotDrop>().Activ)
                {
                    i++;
                    if (i + num >= _listPrefab.Count)
                    {
                        ReturnAmount(amount);
                        return;
                    }
                }
                GameObject obj = Instantiate(info.Prefab, _listPrefab[num + i].transform);
                obj.GetComponent<ItemDrag>().Item = new Item(info, 0);
                if (amount <= obj.GetComponent<ItemDrag>().Item.ItemIn.MaxAmount)
                {
                    obj.GetComponent<ItemDrag>().Item.Amount = amount;
                    amount = 0;
                }
                else if (amount > obj.GetComponent<ItemDrag>().Item.ItemIn.MaxAmount)
                {
                    obj.GetComponent<ItemDrag>().Item.Amount = obj.GetComponent<ItemDrag>().Item.ItemIn.MaxAmount;
                    amount -= obj.GetComponent<ItemDrag>().Item.ItemIn.MaxAmount;
                }
                obj.GetComponentInParent<SlotDrop>().Item = obj.GetComponent<ItemDrag>().Item;
                obj.GetComponent<ItemUI>().AmountText(obj.GetComponent<ItemDrag>().Item.Amount);
                i++;
            }
            while (amount != 0);
            ReturnAmount(0);
        }

        public bool AddAmountItem(DefaultItemInfo info, int amount)
        {
            ItemDrag[] itemUIs = FindObjectsOfType<ItemDrag>();
            for (int i = 0; i < itemUIs.Length; i++)
            {
                Item item = itemUIs[i].Item;
                if (item.ItemIn == info)
                {
                    if (item.Amount + amount <= item.ItemIn.MaxAmount)
                    {
                        item.Amount += amount;
                        ReturnAmount(0);
                        itemUIs[i].GetComponent<ItemUI>().AmountText(item.Amount);
                        return true;
                    }
                    else if (item.Amount + amount > item.ItemIn.MaxAmount)
                    {
                        int m = item.ItemIn.MaxAmount - item.Amount;
                        item.Amount = item.ItemIn.MaxAmount;
                        amount -= m;
                        ReturnAmount(amount);
                        itemUIs[i].GetComponent<ItemUI>().AmountText(item.Amount);
                    }
                }
            }
            return false;
        }

        [Button]
        private void SaveData()
        {
            for (int i = 0; i < _listPrefab.Count; i++)
            {
                SloteInfo slote = new SloteInfo(_listPrefab[i].GetComponent<SlotDrop>().Item, _listPrefab[i].GetComponent<SlotDrop>().Activ);
                _saveManager.Save("Slot_" + i, slote);
            }
        }
        private void LoadData()
        {
            for (int i = 0; i < _inventoryPanelInfo.NumPanel; i++)
            {
                SloteInfo slote = _saveManager.Load<SloteInfo>("Slot_" + i);
                _listPrefab[i].GetComponent<SlotDrop>().Item = slote._item;
                _listPrefab[i].GetComponent<SlotDrop>().Activ = slote._activ;
                if (_listPrefab[i].GetComponent<SlotDrop>().Item.ItemIn != null)
                {
                    GameObject obj = Instantiate(slote._item.ItemIn.Prefab, _listPrefab[i].transform);
                    obj.GetComponent<ItemDrag>().Item = slote._item;
                    obj.GetComponent<ItemDrag>().GetComponent<ItemUI>().AmountText(slote._item.Amount);
                }
            }
        }
        [Button]
        private void ClearSave()
        {
            for (int i = 0; i < _listPrefab.Count; i++)
            {
                SloteInfo slote = new SloteInfo();
                _saveManager.Save("Slot_" + i, slote);
            }
        }
    }
}

