using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Inventory
{
    [System.Serializable]
    public class Item
    {
        [SerializeField, ReadOnly] private DefaultItemInfo _item;
        [SerializeField, ReadOnly] private int _amount;

        public Item()
        {
            _item = null;
            _amount = 0;
        }
        public Item(DefaultItemInfo item, int amount)
        {
            _item = item;
            Amount = amount;
        }

        public DefaultItemInfo ItemIn
        {
            get { return _item; }
        }
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
