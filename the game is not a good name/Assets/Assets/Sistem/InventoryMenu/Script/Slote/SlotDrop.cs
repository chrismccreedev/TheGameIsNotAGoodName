using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace Inventory
{
    public class SlotDrop : MonoBehaviour, IDropHandler
    {
        [SerializeField, ReadOnly] private Item _Item;
        [SerializeField] private bool _activ = false;

        private SlotSpaceChecker _slotSpaceChecker;
        private CanvasGroup _canvasGroup;


        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _slotSpaceChecker = FindObjectOfType<SlotSpaceChecker>();
        }
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        public bool Activ
        {
            get { return _activ; }
            set { _activ = value; }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.GetComponent<ItemDrag>().KeyCode == KeyCode.Mouse0)
            {
                if (eventData.pointerDrag.GetComponent<ItemDrag>() && !_activ && 
                    _slotSpaceChecker.FreeSpaceCheck(eventData.pointerDrag.GetComponent<ItemDrag>().Item.ItemIn, 
                    gameObject.GetComponent<SlotDrop>()))
                {
                    Item = eventData.pointerDrag.GetComponent<ItemDrag>().Item;
                    var itemTransform = eventData.pointerDrag.transform;
                    itemTransform.SetParent(transform);
                    itemTransform.localPosition = Vector3.zero;
                    _slotSpaceChecker.Activate(_Item.ItemIn, gameObject.GetComponent<SlotDrop>());
                }
            }
        }
    }

    public class SloteInfo
    {
        public Item _item;
        public bool _activ;
        public SloteInfo()
        {
            _item = null;
            _activ = false;
        }
        public SloteInfo(Item item, bool activ)
        {
            _item = item;
            _activ = activ;
        }
    }
}
