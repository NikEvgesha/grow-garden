using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private DynamicGridSpawner _grid;


    private List<InventorySlot> _slots;

    private void Awake()
    {
        _slots = new List<InventorySlot>();
    }
    private void Start()
    {
        Inventory.Instance.InventoryUpdate += UpdateUI;
    }

    private void OnDisable()
    {
        Inventory.Instance.InventoryUpdate -= UpdateUI;
    }

    private void UpdateUI(List<Item> items)
    {

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
                //InventorySlot slot = Instantiate(_slotPrefab, _slotsParent);
                InventorySlot slot = _grid.SpawnObject<InventorySlot>(_slotPrefab.gameObject);
                _slots.Add(slot);
            }
        }

            for (int i = 0; i < items.Count; i++)
            {
                if (_slots[i].Item != items[i])
            {
                _slots[i].Init(items[i]);
            }
        }
    }
}