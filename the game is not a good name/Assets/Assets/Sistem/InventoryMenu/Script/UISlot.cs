using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class UISlot : MonoBehaviour, IDropHandler
{
    [SerializeField, ReadOnly] private bool _activ = false;

    public bool Activ
    {
        get { return _activ; }
        set { _activ = value; }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<UIItem>() && !_activ)
        {
            var itemTransform = eventData.pointerDrag.transform;
            itemTransform.SetParent(transform);
            itemTransform.localPosition = Vector3.zero;
            _activ = true;
        }
    }
}
