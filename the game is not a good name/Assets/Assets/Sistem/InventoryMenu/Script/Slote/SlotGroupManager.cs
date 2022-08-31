using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotGroupManager : MonoBehaviour
{
    private GridLayoutGroup _gridLayout;
    private RectTransform _rectTransform;

    private void Start()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        ScaleSlote();
    }
    private void ScaleSlote()
    {
        float x = _rectTransform.rect.width - 80;
        x /= 6f;
        Debug.Log(x);
        //_gridLayout.cellSize.Scale(new Vector2(x, x));
        _gridLayout.cellSize = new Vector2(x, x); 
        Debug.Log(_gridLayout.cellSize);
    }
}
