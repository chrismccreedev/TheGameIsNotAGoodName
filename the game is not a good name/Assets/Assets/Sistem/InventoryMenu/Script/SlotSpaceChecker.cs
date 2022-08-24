using UnityEngine;

public class SlotSpaceChecker : MonoBehaviour
{
    [SerializeField] private Inventory.SlotDrop[] _listPrefab;

    private void Start()
    {
        Inventory.SlotDrop[] list = FindObjectsOfType<Inventory.SlotDrop>();
        _listPrefab = new Inventory.SlotDrop[list.Length];
        for(int i = 0; i < list.Length; i++)
        {
            _listPrefab[i] = list[(list.Length - 1) - i];
        }
    }
    public int FreeSpaceCheck(DefaultItemInfo info)
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

    public bool FreeSpaceCheck(DefaultItemInfo info, Inventory.SlotDrop uISlot)
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
                if (_listPrefab[num + x + (y * 6)].GetComponent<Inventory.SlotDrop>().Activ == true)
                {
                    return false;
                }
            }
        }
        Debug.Log(true);
        return true;
    }
    public void Activate(DefaultItemInfo info, Inventory.SlotDrop uISlot)
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
                _listPrefab[num + x + (y * 6)].GetComponent<Inventory.SlotDrop>().Activ = true;
                _listPrefab[num + x + (y * 6)].CanvasGroup.blocksRaycasts = false;
                _listPrefab[num + x + (y * 6)].CanvasGroup.alpha = 0;
                if (x == 0 && y == 0)
                {
                    _listPrefab[num + x + (y * 6)].CanvasGroup.blocksRaycasts = true;
                    _listPrefab[num + x + (y * 6)].CanvasGroup.alpha = 1;
                }
            }
        }
    }
    public void Disable(DefaultItemInfo info, Inventory.SlotDrop uISlot)
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
                _listPrefab[num + x + (y * 6)].GetComponent<Inventory.SlotDrop>().Activ = false;
                _listPrefab[num + x + (y * 6)].CanvasGroup.blocksRaycasts = true;
                _listPrefab[num + x + (y * 6)].CanvasGroup.alpha = 1;
            }
        }
    }

    public int CheckForAnItem(DefaultItemInfo info)
    {
        for(int i = 0; i < _listPrefab.Length; i++)
        {
            if (_listPrefab[i].Item?.ItemIn == info && _listPrefab[i].Item?.Amount < info.MaxAmount)
            {
                return i;
            }
        }
        return -1;
    }

    private void  CreateNewMass()
    {
        Inventory.SlotDrop[] listPrefab = FindObjectsOfType<Inventory.SlotDrop>();
        _listPrefab = new Inventory.SlotDrop[listPrefab.Length];

        for(int i = 0; i < listPrefab.Length; i++)
        {
            _listPrefab[i] = listPrefab[(listPrefab.Length - 1) - i];
        }
    }
}
