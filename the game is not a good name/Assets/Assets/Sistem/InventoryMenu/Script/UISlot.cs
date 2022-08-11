using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<UIItem>())
        {
            var itemTransform = eventData.pointerDrag.transform;
            itemTransform.SetParent(transform);
            itemTransform.localPosition = Vector3.zero;
        }
    }
}
