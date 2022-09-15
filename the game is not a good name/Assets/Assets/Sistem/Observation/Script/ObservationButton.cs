using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObservationButton : MonoBehaviour
{
    private Button _button;
    private TargetAcquisition _targetAcquisition;

    private void Start()
    {
        _targetAcquisition = FindObjectOfType<TargetAcquisition>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Eneble);
    }
    public void Eneble()
    {
        _targetAcquisition.EnableCheck();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Disable);
    }
    public void Disable()
    {
        _targetAcquisition.DisableCheck();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Eneble);
    }
}
