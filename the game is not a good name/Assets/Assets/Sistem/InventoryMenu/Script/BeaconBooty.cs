using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BeaconBooty : MonoBehaviour
{
    [SerializeField] private ItemInfo _info;
    [SerializeField] private bool _enfinityItem;
    [SerializeField] private bool _randomAmount;
    [ShowIf("_randomAmount")]
    [SerializeField] private int _minAmount;
    [ShowIf("_randomAmount")]
    [SerializeField] private int _maxAmount;
    [HideIf("_randomAmount")]
    [SerializeField] private int _startAmount;

    private int _amount;
    public ItemInfo Info => _info;

    public int Amount
    {
        get
        {
            if(_enfinityItem && _amount == 0)
            {
                CreateAmount();
            }
            return _amount;
        }
        set
        {
            if(value == 0)
            {
                if(_enfinityItem)
                {
                    _amount = 0;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if(value < 0)
            {
            }
            else if (value > 0)
            {
                _amount = value;
            }
        }
    }

    private void Start()
    {
        CreateAmount();
    }

    private void CreateAmount()
    {
        if (!_randomAmount)
        {
            _amount = _startAmount;
        }
        else
        {
            _amount = Random.Range(_minAmount, _maxAmount + 1);
        }
    }
}
