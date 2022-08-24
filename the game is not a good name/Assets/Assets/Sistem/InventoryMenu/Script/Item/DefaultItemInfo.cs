using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "newDefaultItem", menuName = "InventoryManager/ItemInfo/Default")]
public class DefaultItemInfo : ScriptableObject
{
    [BoxGroup("InventoryManager UI")]
    [SerializeField] private string _name;
    [BoxGroup("Game Object")]
    [SerializeField] private GameObject _object;
    [BoxGroup("InventoryManager UI")]
    [SerializeField] private Sprite _objectIcon;
    [BoxGroup("InventoryManager UI")]
    [SerializeField] private GameObject _prefab;
    [BoxGroup("InventoryManager UI")]
    [SerializeField] private int _numPanelX;
    [BoxGroup("InventoryManager UI")]
    [SerializeField] private int _numPanelY;
    [BoxGroup("InventoryManager UI")]
    [SerializeField] private int _maxAmount;

    public string Name => _name;
    public GameObject Object => _object;
    public Sprite ObjectIcon => _objectIcon;
    public GameObject Prefab => _prefab;
    public int NumPanelX => _numPanelX;
    public int NumPanelY => _numPanelY;
    public int MaxAmount => _maxAmount;
}
