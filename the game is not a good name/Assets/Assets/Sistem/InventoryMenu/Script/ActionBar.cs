using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ActionBar : MonoBehaviour, IPointerEnterHandler
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        [SerializeField]private RectTransform _childrenRectTransform;
        private bool _trigger;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            
            ClosePanel();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_trigger == true)
            {
                ClosePanel();
            }
        }

        public void OpenPanel(Vector2 position)
        {
            _trigger = false;
            Vector2 newPosition;
            newPosition.x = (position.x + (_rectTransform.sizeDelta.x / 2)) - (GetComponent<VerticalLayoutGroup>().padding.left + 5);
            
            if(newPosition.x + (_childrenRectTransform.sizeDelta.x / 2) >= Screen.width)
            {
                newPosition.x = (position.x - (_rectTransform.sizeDelta.x / 2)) + (GetComponent<VerticalLayoutGroup>().padding.right + 5);
            }
            newPosition.y = (position.y - (_rectTransform.sizeDelta.y / 2)) + (GetComponent<VerticalLayoutGroup>().padding.top + 5);
            Debug.Log(newPosition.y);
            if(newPosition.y - (_childrenRectTransform.sizeDelta.y / 2) <= 0)
            {
                newPosition.y = (position.y + (_rectTransform.sizeDelta.y / 2)) - (GetComponent<VerticalLayoutGroup>().padding.top + 5);
            }

            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.gameObject.transform.position = newPosition;
            _canvasGroup.alpha = 1;
            StartCoroutine(PointPause());
        }
        public void ClosePanel()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
        private IEnumerator PointPause()
        {
            yield return new WaitForSeconds(0.001f);
            _trigger = true;
        }
    }
}
