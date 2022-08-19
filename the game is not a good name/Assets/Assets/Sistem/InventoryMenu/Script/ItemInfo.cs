using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "newDefaultItem", menuName = "Inventory/ItemInfo/Default")]
public class ItemInfo : ScriptableObject
{
    [BoxGroup("Inventory UI")]
    [SerializeField] private string _name;
    [BoxGroup("Game Object")]
    [SerializeField] private GameObject _object;
    [BoxGroup("Inventory UI")]
    [SerializeField] private Image _objectIcon;
    [BoxGroup("Inventory UI")]
    [SerializeField] private GameObject _prefab;
    [BoxGroup("Inventory UI")]
    [SerializeField] private int _numPanelX;
    [BoxGroup("Inventory UI")]
    [SerializeField] private int _numPanelY;
    [BoxGroup("Inventory UI")]
    [SerializeField] private int _maxAmount;

    public string Name => _name;
    public GameObject Object => _object;
    public Image ObjectIcon => _objectIcon;
    public GameObject Prefab => _prefab;
    public int NumPanelX => _numPanelX;
    public int NumPanelY => _numPanelY;
    public int MaxAmount => _maxAmount;
}
