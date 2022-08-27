using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootyUI : MonoBehaviour
{
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void Open()
    {
        _canvas.enabled = true;
    }
    public void Close()
    {
        _canvas.enabled = false;
    }
}
