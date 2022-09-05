using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TestMove : MonoBehaviour
{
    [SerializeField] private GameObject _obj;

    [BoxGroup("Rotate Move")]
    [HideIf("_moveX")]
    [SerializeField] private bool _moveRotate;
    [BoxGroup("Rotate Move")]
    [ShowIf("_moveRotate")]
    [MinValue(0.01f)]
    [SerializeField] private float _roteSpeed;
    [BoxGroup("Rotate Move")]
    [ShowIf("_moveRotate")]
    [SerializeField]private float _radius;

    [BoxGroup("Move X")]
    [HideIf("_moveRotate")]
    [SerializeField] private bool _moveX;
    [BoxGroup("Move X")]
    [ShowIf("_moveX")]
    [MinValue(0.01f)]
    [SerializeField] private float _speedX;
    [BoxGroup("Move X")]
    [ShowIf("_moveX")]
    [SerializeField] private float _shiftX;

    [BoxGroup("Move Y")]
    [SerializeField] private bool _moveY;
    [BoxGroup("Move Y")]
    [ShowIf("_moveY")]
    [MinValue(0.01f)]
    [SerializeField] private float _speedY;
    [BoxGroup("Move Y")]
    [ShowIf("_moveY")]
    [SerializeField] private float _shiftY;

    private Coroutine _CR_MoveRotate;
    private Coroutine _CR_MoveY;
    private Coroutine _CR_MoveX;

    private void Start()
    {
        MoveManeger();
    }
    [Button]
    private void MoveManeger()
    {
        if(_moveRotate && _CR_MoveRotate == null)
        {
            _CR_MoveRotate = StartCoroutine(RotateMove());
        }
        else if (!_moveRotate && _CR_MoveRotate != null)
        {
            _obj.transform.localPosition = Vector3.zero;
            StopCoroutine(_CR_MoveRotate);
            _CR_MoveRotate = null;
        }

        if(_moveY && _CR_MoveY == null)
        {
            _CR_MoveY = StartCoroutine(MoveY());
        }
        else if(!_moveY && _CR_MoveY != null)
        {
            StopCoroutine(_CR_MoveY);
            _CR_MoveY = null;
        }

        if(_moveX && _CR_MoveX == null)
        {
            _CR_MoveX = StartCoroutine(MoveX());
        }
        else if (!_moveX && _CR_MoveX != null)
        {
            StopCoroutine(_CR_MoveX);
            _CR_MoveX = null;
        }
    }
    private IEnumerator RotateMove()
    {
        _obj.transform.localPosition = Vector3.zero;
        _obj.transform.localPosition += new Vector3(_radius, 0, 0);
        float length = 2f * Mathf.PI * _radius;
        float time = length / _roteSpeed;
        while (true)
        {
            transform.DORotate(new Vector3(transform.localRotation.x, transform.localRotation.y + 360, transform.localRotation.z), time, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
        }
    }
    private IEnumerator MoveY()
    {
        float startPos = _obj.transform.localPosition.y;
        float finishPos = startPos + _shiftY;
        float length = Mathf.Abs(finishPos - startPos);
        float time = length / (_speedY);
        while (true)
        {
            _obj.transform.DOLocalMoveY(finishPos, time/2).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time / 2);
            _obj.transform.DOLocalMoveY(startPos, time / 2).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time / 2);
        }
    }
    private IEnumerator MoveX()
    {
        float startPos = _obj.transform.localPosition.x;
        float finishPos = startPos + _shiftX;
        float length = Mathf.Abs(finishPos - startPos);
        float time = length / (_speedX);
        while (true)
        {
            _obj.transform.DOLocalMoveX(finishPos, time / 2).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time / 2);
            _obj.transform.DOLocalMoveX(startPos, time / 2).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time / 2);
        }
    }
}
