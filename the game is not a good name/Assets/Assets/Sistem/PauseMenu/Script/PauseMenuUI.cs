using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{
    [BoxGroup("Background")]
    [SerializeField] private GameObject _panelBackground;
    [BoxGroup("Background")]
    [SerializeField] private float _timeFaidBackground;
    [BoxGroup("Background")]
    [SerializeField] private float _alfaBackground;
    [BoxGroup("Left Panel")]
    [SerializeField] private GameObject _leftPanel;
    [BoxGroup("Left Panel")]
    [SerializeField] private float _shift;
    [BoxGroup("Left Panel")]
    [SerializeField] private float _timeLeftPanel;

    private Canvas _canvas;
    private Image _imagePanelBackground;

    private Coroutine _corutineOpen;
    private Coroutine _corutineClose;

    private float _startPosLeftPanel;
    private float _finishPosLeftPanel;

    private void Start()
    {
        _finishPosLeftPanel = _leftPanel.transform.localPosition.x;
        _startPosLeftPanel = _finishPosLeftPanel - _shift;
        _imagePanelBackground = _panelBackground.GetComponent<Image>();

        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;

        _imagePanelBackground.DOFade(0, 0);
        _leftPanel.transform.DOLocalMoveX(_startPosLeftPanel, 0);
    }
    public void OpenPause()
    {
        if(_corutineClose != null)
            StopCoroutine(_corutineClose);
        StartCoroutine(CROpenPause());
    }
    public void ClosePause()
    {
        if(_corutineClose != null)
            StopCoroutine(_corutineOpen);
        StartCoroutine(CRClosePause());
    }

    private IEnumerator CROpenPause()
    {
        _canvas.enabled = true;
        _imagePanelBackground.DOFade(_alfaBackground, _timeFaidBackground);
        yield return new WaitForSeconds(_timeFaidBackground);
        _leftPanel.transform.DOLocalMoveX(_finishPosLeftPanel, _timeLeftPanel);
    }

    private IEnumerator CRClosePause()
    {
        _leftPanel.transform.DOLocalMoveX(_startPosLeftPanel, _timeLeftPanel);
        yield return new WaitForSeconds(_timeLeftPanel);
        _imagePanelBackground.DOFade(0, _timeFaidBackground);
        yield return new WaitForSeconds(_timeFaidBackground);
        _canvas.enabled = false;
    }
}
