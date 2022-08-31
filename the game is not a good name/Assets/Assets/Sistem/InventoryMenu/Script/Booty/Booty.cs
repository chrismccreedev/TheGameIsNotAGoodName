using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booty : MonoBehaviour
{
    [SerializeField] private KeyInfo _keyInfo;

    private SlotSpaceChecker _slotSpaceChecker;
    private DefaultItemInfo _info = null;
    private Inventory.InventoryManager _inventory;
    private BootyUI _bootyUI;
    private bool _activ = false;
    private BeaconBooty _beaconBooty;

    private void Start()
    {
        _slotSpaceChecker = FindObjectOfType<SlotSpaceChecker>();
        _bootyUI = FindObjectOfType<BootyUI>();
        _inventory = FindObjectOfType<Inventory.InventoryManager>();
        _inventory.ReturnAmount += Amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BeaconBooty>())
        {
            _beaconBooty = other.GetComponent<BeaconBooty>();
            _info = _beaconBooty.Info;
            _activ = true;
            _bootyUI.Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BeaconBooty>())
        {
            _beaconBooty = null;
            _info = null;
            _activ = false;
            _bootyUI.Close();
        }
    }

    private void Update()
    {
        if(_beaconBooty == null)
        {
            _info = null;
            _activ = false;
            _bootyUI.Close();
        }
    }
    public void Inventory()
    {
        int value = _slotSpaceChecker.CheckForAnItem(_info);
        if (value >= 0)
        {
            bool check = _inventory.AddAmountItem(_info, _beaconBooty.Amount);
            if (check)
            {
                return;
            }
        }

        int num = _slotSpaceChecker.FreeSpaceCheck(_info);
        if (num != -1)
        {
            _inventory.AddItem(_info, num, _beaconBooty.Amount);
        }
        else
        {
            Debug.Log("Нету места");
        }
    }

    private void Amount(int amount)
    {
        _beaconBooty.Amount = amount;
    }
}
