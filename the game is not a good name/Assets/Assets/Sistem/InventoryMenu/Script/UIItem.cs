using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private Transform _oldParents;
    private SlotSpaceChecker _slotSpaceChecker;

    [SerializeField, ReadOnly] private UISlot _slot = null;
    [SerializeField, ReadOnly] private ItemInfo _item;
    [SerializeField, ReadOnly] private int _amount;

    public ItemInfo Item
    {
        get { return _item; }
        set { _item = value; }
    }
    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _slot = GetComponentInParent<UISlot>();
        _slotSpaceChecker = GetComponentInParent<SlotSpaceChecker>();
        _slotSpaceChecker.Activate(_item, _slot);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _slot = GetComponentInParent<UISlot>();

        _slot.UIItem = null;

        _slotSpaceChecker.Disable(_item, _slot);
        _oldParents = _slot.transform;
        transform.SetParent(_canvas.transform);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!transform.parent.GetComponent<UISlot>())
        {
            transform.SetParent(_oldParents);
            _slotSpaceChecker.Activate(_item, _slot);
            _slot.UIItem = GetComponent<UIItem>();
        }
        
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
}
