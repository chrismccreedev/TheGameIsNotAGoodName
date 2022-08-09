using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "InventoryInfo", menuName = "InventoryInfo")]
public class InventoryInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _objectIcon;
    [SerializeField] private int _numPanelX;
    [SerializeField] private int _numPanelY;
}
