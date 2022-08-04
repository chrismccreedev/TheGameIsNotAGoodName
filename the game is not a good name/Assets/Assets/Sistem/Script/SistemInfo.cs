using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SistemInfo", menuName = "SistemInfo")]
public class SistemInfo : ScriptableObject
{
    [SerializeField, ReadOnly] private bool _pauseValue;
    public KeyCode _pause;

    public bool PauseValue => _pauseValue;

    [Button]
    public void Pause()
    {
        _pauseValue = !_pauseValue;
    }
}
