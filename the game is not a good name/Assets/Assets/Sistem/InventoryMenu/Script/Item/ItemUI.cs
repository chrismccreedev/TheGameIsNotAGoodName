using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;


namespace Inventory
{
    public class ItemUI : MonoBehaviour
    {
        [BoxGroup("UI")]
        [SerializeField] private TextMeshProUGUI _text;
        [BoxGroup("UI")]
        [SerializeField] private Image _image;


        public void ItemImaje(Item item)
        {
            Debug.Log(item);
            _image.sprite = item.ItemIn.ObjectIcon;
        }

        public void AmountText(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}
