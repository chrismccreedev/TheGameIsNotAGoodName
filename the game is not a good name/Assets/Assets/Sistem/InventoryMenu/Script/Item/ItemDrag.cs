using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace Inventory
{
    public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField, ReadOnly] private SlotDrop _slot = null;
        [SerializeField] private Item _item;

        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _oldParents;
        private SlotSpaceChecker _slotSpaceChecker;
        private ItemUI _itemUI;

        private KeyCode _keyCode;

        public KeyCode KeyCode => _keyCode;
        public Item Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private void Start()
        {
            _itemUI = GetComponent<ItemUI>();
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _slot = GetComponentInParent<SlotDrop>();
            _slotSpaceChecker = GetComponentInParent<SlotSpaceChecker>();
            _slotSpaceChecker.Activate(_item.ItemIn, _slot);

            _itemUI.X = _item.ItemIn.NumPanelX;
            _itemUI.Y = _item.ItemIn.NumPanelY;
            _itemUI.ItemImaje(_item);

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _keyCode = KeyCode.Mouse0;
                _slot = GetComponentInParent<SlotDrop>();

                _slot.Item = null;

                _slotSpaceChecker.Disable(_item.ItemIn, _slot);
                _oldParents = _slot.transform;
                transform.SetParent(_canvas.transform);
                _canvasGroup.blocksRaycasts = false;
            }
            else
            {
                _keyCode = KeyCode.None;
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_keyCode == KeyCode.Mouse0)
            {
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_keyCode == KeyCode.Mouse0)
            {
                if (!transform.parent.GetComponent<SlotDrop>())
                {
                    transform.SetParent(_oldParents);
                    _slotSpaceChecker.Activate(_item.ItemIn, _slot);
                    _slot.Item = GetComponent<ItemDrag>().Item;
                }

                transform.localPosition = Vector3.zero;
                _canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
