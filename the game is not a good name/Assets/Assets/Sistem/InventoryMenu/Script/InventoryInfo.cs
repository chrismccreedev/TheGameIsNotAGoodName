using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "InventoryInfo", menuName = "Inventory/InventoryInfo")]
public class InventoryInfo : ScriptableObject
{
    [SerializeField, ReadOnly] private int _num;
    [SerializeField] private string _name;
    [SerializeField] private Image _objectIcon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _numPanelX;
    [SerializeField] private int _numPanelY;
    [SerializeField] private int _maxStack;

    public int Num => _num;
    public string Name => _name;
    public Image ObjectIcon => _objectIcon;
    public GameObject Prefab => _prefab;
    public int NumPanelX => _numPanelX;
    public int NumPanelY => _numPanelY;
    public int MaxStack => _maxStack;
}
