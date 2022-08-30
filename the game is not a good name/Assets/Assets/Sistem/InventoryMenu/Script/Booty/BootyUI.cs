using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BootyUI : MonoBehaviour
{
    [SerializeField] private float _shift;
    [SerializeField] private float _time;

    private float _startPosition;
    private float _endPosition;
    private void Start()
    {
        _endPosition = transform.localPosition.x;
        _startPosition = transform.localPosition.x + _shift;
        transform.DOLocalMoveX(_startPosition, 0);
    }

    public void Open()
    {
        transform.DOLocalMoveX(_endPosition, _time);
    }
    public void Close()
    {
        transform.DOLocalMoveX(_startPosition, _time);
    }
}
