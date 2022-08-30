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

        [SerializeField] private RectTransform _rectTransform;

        private int x;
        private int y;

        private GridLayoutGroup _group;

        public int X
        {
            set
            {
                x = value;
            }
        }
        public int Y
        {
            set
            {
                y = value;
            }
        }

        public void ItemImaje(Item item)
        {
            _group = GetComponentInParent<GridLayoutGroup>();
            _image.sprite = item.ItemIn.ObjectIcon;
            float size = _group.cellSize.x;
            float sizeX = size * x + 10 * (x - 1);
            float sizeY = size * y + 10 * (y - 1);
            _rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            float positionX = sizeX/2f;
            float positionY = -sizeY/2f;
            Debug.Log("X: " + positionX + "; Y: " + positionY);
            _rectTransform.anchoredPosition = new Vector2(positionX, positionY);
        }

        public void AmountText(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}
