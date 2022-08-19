using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class UISlot : MonoBehaviour, IDropHandler
{
    [SerializeField, ReadOnly] private UIItem _UIItem;
    [SerializeField] private bool _activ = false;

    private SlotSpaceChecker _slotSpaceChecker;

    private void Start()
    {
        _slotSpaceChecker = FindObjectOfType<SlotSpaceChecker>();
    }
    public UIItem UIItem
    {
        get { return _UIItem; }
        set { _UIItem = value; }
    }
    public bool Activ
    {
        get { return _activ; }
        set { _activ = value; }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<UIItem>() && !_activ && _slotSpaceChecker.FreeSpaceCheck(eventData.pointerDrag.GetComponent<UIItem>().Item, gameObject.GetComponent<UISlot>()))
        {
            UIItem = eventData.pointerDrag.GetComponent<UIItem>();
            var itemTransform = eventData.pointerDrag.transform;
            itemTransform.SetParent(transform);
            itemTransform.localPosition = Vector3.zero;
            _slotSpaceChecker.Activate(_UIItem.Item, gameObject.GetComponent<UISlot>());
        }
    }
}
