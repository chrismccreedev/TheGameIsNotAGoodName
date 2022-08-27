using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace Inventory
{

    public class ItemClick : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField, ReadOnly] private ActionBar _actionBar;
        private void Start()
        {
            _actionBar = FindObjectOfType<ActionBar>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                _actionBar.OpenPanel(eventData.position);
            }
        }
    }
}
