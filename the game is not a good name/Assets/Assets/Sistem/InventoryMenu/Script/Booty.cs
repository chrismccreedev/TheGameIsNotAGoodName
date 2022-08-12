using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booty : MonoBehaviour
{
    [SerializeField] private KeyInfo _keyInfo;

    private InventoryInfo _info = null;
    private Inventory _inventory;
    private BootyUI _bootyUI;
    private bool _activ = false;

    private void Start()
    {
        _bootyUI = FindObjectOfType<BootyUI>();
        _inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BeaconBooty>())
        {
            _info = other.GetComponent<BeaconBooty>().Info;
            _activ = true;
            _bootyUI.Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _info = null;
        _activ = false;
        _bootyUI.Close();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_keyInfo._keyApply) && _activ)
        {
            Debug.Log("Yes, you are not stupid!");
            _inventory.Addobject(_info);
        }
    }
}
