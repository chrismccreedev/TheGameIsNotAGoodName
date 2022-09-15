using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyBeacon : MonoBehaviour
{
    [SerializeField, ReadOnly] private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 5;
        _outline.enabled = false;
    }

    public void EnableOutline()
    {
        _outline.enabled = true;
    }
    public void DisableOutline()
    {
        _outline.enabled = false;
    }
}
