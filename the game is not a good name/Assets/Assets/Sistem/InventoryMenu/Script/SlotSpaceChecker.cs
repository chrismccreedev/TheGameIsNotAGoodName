using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSpaceChecker : MonoBehaviour
{
    [SerializeField] private UISlot[] _listPrefab;

    public int FreeSpaceCheck(ItemInfo info)
    {
        CreateNewMass();

        for (int i = 0; i < _listPrefab.Length; i++)
        {
            bool flag = true;
            for (int x = 0; x < info.NumPanelX; x++)
            {
                for (int y = 0; y < info.NumPanelY; y++)
                {
                    if (i + x + (y * 6) >= _listPrefab.Length)
                    {
                        return -1;
                    }
                    if ((i + x + (y * 6)) % 6 == 0 && x != 0)
                    {
                        flag = false;
                    }
                    if (_listPrefab[i + x + y * 6].Activ)
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                return i;
            }
        }
        return -1;
    }

    public bool FreeSpaceCheck(ItemInfo info, UISlot uISlot)
    {
        int num = 0;
        for (int i = 0; i < _listPrefab.Length; i++)
        {
            if (_listPrefab[i] == uISlot)
            {
                num = i;
                break;
            }
        }

        for (int x = 0; x < info.NumPanelX; x++)
        {
            for (int y = 0; y < info.NumPanelY; y++)
            {
                if (num + x + (y * 6) >= _listPrefab.Length)
                {
                    return false;
                }
                if ((num + x + (y * 6)) % 6 == 0 && x != 0)
                {
                    return false;
                }
                if (_listPrefab[num + x + (y * 6)].GetComponent<UISlot>().Activ == true)
                {
                    return false;
                }
            }
        }
        Debug.Log(true);
        return true;
    }
    public void Activate(ItemInfo info, UISlot uISlot)
    {
        int num = 0;
        for(int i = 0; i < _listPrefab.Length; i++)
        {
            if (_listPrefab[i] == uISlot)
            {
                num = i;
                break;
            }
        }

        for (int x = 0; x < info.NumPanelX; x++)
        {
            for (int y = 0; y < info.NumPanelY; y++)
            {
                _listPrefab[num + x + (y * 6)].GetComponent<UISlot>().Activ = true;
            }
        }
    }
    public void Disable(ItemInfo info, UISlot uISlot)
    {
        int num = 0;
        for (int i = 0; i < _listPrefab.Length; i++)
        {
            if (_listPrefab[i] == uISlot)
            {
                num = i;
                break;
            }
        }

        for (int x = 0; x < info.NumPanelX; x++)
        {
            for (int y = 0; y < info.NumPanelY; y++)
            {
                _listPrefab[num + x + (y * 6)].GetComponent<UISlot>().Activ = false;
            }
        }
    }

    public int CheckForAnItem(ItemInfo info)
    {
        for(int i = 0; i < _listPrefab.Length; i++)
        {
            if (_listPrefab[i].UIItem?.Item == info && _listPrefab[i].UIItem?.Amount < info.MaxAmount)
            {
                return i;
            }
        }
        return -1;
    }

    private void  CreateNewMass()
    {
        UISlot[] listPrefab = FindObjectsOfType<UISlot>();
        _listPrefab = new UISlot[listPrefab.Length];

        for(int i = 0; i < listPrefab.Length; i++)
        {
            _listPrefab[i] = listPrefab[(listPrefab.Length - 1) - i];
        }
    }
}
