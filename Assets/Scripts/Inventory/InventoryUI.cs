using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private DynamicGridSpawner _grid;
    [SerializeField] private GameObject _mainUI;



    private List<InventorySlot> _slotsUsable;
    private List<InventorySlot> _slotsMain;

    private void Awake()
    {
        _slotsUsable = new List<InventorySlot>();
        _slotsMain = new List<InventorySlot>();
    }
    private void Start()
    {
        Inventory.Instance.InventoryUsableUpdate += UpdateUI;
        Inventory.Instance.InventoryMainUpdate += UpdateMainUI;


        for (int i = 0; i < Inventory.Instance.CapacityUsable; i++)
        {
            InventorySlot slot = Instantiate(_slotPrefab, _slotsParent);
            slot.Init(null);
            _slotsUsable.Add(slot);
        }

        for (int i = 0; i < Inventory.Instance.CapacityMain; i++)
        {
            InventorySlot slot = _grid.SpawnObject<InventorySlot>(_slotPrefab.gameObject);
            slot.Init(null);
            _slotsMain.Add(slot);
        }
    }

    private void OnDisable()
    {
        Inventory.Instance.InventoryUsableUpdate -= UpdateUI;
        Inventory.Instance.InventoryMainUpdate -= UpdateMainUI;
    }

    private void UpdateUI(List<Item> items)
    {
        /*
                if (_slots.Count > items.Count)
                {
                    int slotToRemove = _slots.Count - items.Count;
                    for (int i = 0; i < slotToRemove; i++)
                    {
                        Destroy(_slots[_slots.Count - i - 1].gameObject);
                    }
                    _slots.RemoveRange(items.Count, _slots.Count - items.Count);
                } else if (_slots.Count < items.Count)
                {
                    int slotToCreate = items.Count - _slots.Count;
                    for (int i = 0; i < slotToCreate; i++)
                    {
                        InventorySlot slot = Instantiate(_slotPrefab, _slotsParent);

                        _slots.Add(slot);
                    }
                }*/

        for (int i = 0; i < _slotsUsable.Count; i++)
        {
            if (i >= items.Count)
            {
                _slotsUsable[i].Init(null);
            }
            else
            {
                _slotsUsable[i].Init(items[i]);
            }
        }
    }


    private void UpdateMainUI(List<Item> items)
    {
        for (int i = 0; i < _slotsMain.Count; i++)
        {
            if (i >= items.Count)
            {
                _slotsMain[i].Init(null);
            }
            else
            {
                _slotsMain[i].Init(items[i]);
            }
        }
    }


    public void ToggleMainUI()
    {
        _mainUI.SetActive(!_mainUI.activeInHierarchy);
    }
}