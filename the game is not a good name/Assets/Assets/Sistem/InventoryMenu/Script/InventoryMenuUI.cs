using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class InventoryMenuUI : MonoBehaviour
{
    [BoxGroup("Background")]
    [SerializeField] private GameObject _panelBackground;
    [BoxGroup("Background")]
    [SerializeField] private float _alfaBackground;
    [BoxGroup("Background")]
    [SerializeField] private float _timeFaidBackground;
    [BoxGroup("Panel")]
    [BoxGroup("Panel/Right Panel")]
    [SerializeField] private GameObject _rightPanel;
    [BoxGroup("Panel/Right Panel")]
    [SerializeField] private float _shiftRightPanel;
    [BoxGroup("Panel/Left Panel")]
    [SerializeField] private GameObject _leftPanel;
    [BoxGroup("Panel/Left Panel")]
    [SerializeField] private float _shiftLeftPanel;
    [BoxGroup("Panel")]
    [SerializeField] private float _timePanel;

    private Canvas _canvas;
    private Image _imagePanelBackground;

    private Coroutine _corutineOpen;
    private Coroutine _corutineClose;

    private float _startPosRightPanel;
    private float _finishPosRightPanel;
    private float _startPosLeftPanel;
    private float _finishPosLeftPanel;


    private void Start()
    {
        _finishPosLeftPanel = _leftPanel.transform.localPosition.y;
        _finishPosRightPanel = _rightPanel.transform.localPosition.y;
        _startPosLeftPanel = _finishPosLeftPanel + _shiftLeftPanel;
        _startPosRightPanel = _finishPosRightPanel + _shiftRightPanel;
        _imagePanelBackground = _panelBackground.GetComponent<Image>();

        _leftPanel.transform.DOLocalMoveY(_startPosLeftPanel, 0);
        _rightPanel.transform.DOLocalMoveY(_startPosRightPanel, 0);
        _imagePanelBackground.DOFade(0, 0);
        
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }
    public void OpenPause()
    {
        if (_corutineClose != null)
            StopCoroutine(_corutineClose);
        _corutineOpen = StartCoroutine(CROpenPause());
    }
    public void ClosePause()
    {
        if( _corutineOpen != null)
            StopCoroutine(_corutineOpen);
        _corutineClose = StartCoroutine(CRClosePause());
    }
    private IEnumerator CROpenPause()
    {
        _canvas.enabled = true;
        _imagePanelBackground.DOFade(_alfaBackground, _timeFaidBackground);
        _leftPanel.transform.DOLocalMoveY(_finishPosLeftPanel, _timePanel);
        _rightPanel.transform.DOLocalMoveY(_finishPosRightPanel, _timePanel);

        yield return new WaitForSeconds(0);
    }

    private IEnumerator CRClosePause()
    {
        _rightPanel.transform.DOLocalMoveY(_startPosRightPanel, _timePanel);
        _leftPanel.transform.DOLocalMoveY(_startPosLeftPanel, _timePanel);
        _imagePanelBackground.DOFade(0, _timeFaidBackground);
        yield return new WaitForSeconds(_timeFaidBackground);
        _canvas.enabled = false;
    }
}
