using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using static VoxelImporter.StructureData;

[Serializable]
public class InventoryItem
{
    public Item item;
    public int amount;
    public bool active;
}

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance => _instance;

    [SerializeField] private int _capacityUsable = 10;
    [SerializeField] private int _capacityMain = 50;
    [SerializeField] private Transform _handPoint;
    [SerializeField] private Transform _itemsParent;

    private List<InventoryItem> _itemsUsable;
    private List<InventoryItem> _itemsMain;
    private int _currentActiveIdx = -1;

    public int CapacityUsable => _capacityUsable;
    public int CapacityMain => _capacityMain;

    public Action<List<InventoryItem>> InventoryUsableUpdate; // Изменено на InventoryItem
    public Action<List<InventoryItem>> InventoryMainUpdate;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _itemsUsable = Enumerable.Repeat<InventoryItem>(null, _capacityMain).ToList();
        _itemsMain = Enumerable.Repeat<InventoryItem>(null, _capacityUsable).ToList();
    }

    public bool Add(Item item)
    {
        bool added = false;
        int idx;

        if ((item is HarvestPlantItem || item is FishItem) && (idx = CheckEmptySlot(true)) != -1)
        {
            _itemsMain[idx] = new InventoryItem { item = item, amount = 1, active = false };
            InventoryMainUpdate?.Invoke(_itemsMain);
            added = true;
        }
        else if (item is SeedItem seedItem)
        {
            int index = _itemsUsable.FindIndex(x => x != null && x.item.Data == item.Data);
            if (index >= 0)
            {
                var existingItem = _itemsUsable[index];
                existingItem.amount++;
                _itemsUsable[index] = existingItem;
                Destroy(item.gameObject);
                added = false;
            }
            else if ((idx = CheckEmptySlot(false)) != -1)
            {
                _itemsUsable[idx] = new InventoryItem { item = item, amount = 1, active = false };
                added = true;
            }
            InventoryUsableUpdate?.Invoke(_itemsUsable);
            
        }

        if (added)
        {
            item.transform.SetParent(_itemsParent);
            item.gameObject.SetActive(false);
        }

        return added;
    }
    public bool RemoveInActive()
    {
        if (_currentActiveIdx == -1 || _currentActiveIdx >= _itemsUsable.Count) 
            return false;

        _itemsUsable[_currentActiveIdx].amount--;
        if (_itemsUsable[_currentActiveIdx].amount == 0)
        {
            Destroy(_itemsUsable[_currentActiveIdx].item.gameObject);
            _itemsUsable[_currentActiveIdx] = null;
            _currentActiveIdx = -1;
        }
            
        InventoryUsableUpdate?.Invoke(_itemsUsable);
        return true;

    }

    public int CheckEmptySlot(bool inMainInventory)
    {
        int res = -1;
        List<InventoryItem> list = inMainInventory ? _itemsMain : _itemsUsable;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                res = i;
                break;
            }
        }
        if (res == -1)
            GameUI.Instance.Hints.ShowHint(UIHintType.NoSpaceInInventory);
        return res;
    }

    public ReadOnlyCollection<InventoryItem> GetItemsByType(ItemType type)
    {
        return _itemsMain.FindAll(x => x != null && x.item.Type == type).AsReadOnly();
    }

    public ReadOnlyCollection<InventoryItem> GetUsableItemsByType(ItemType type)
    {
        return _itemsUsable.FindAll(x => x != null && x.item.Type == type).AsReadOnly();
    }

    public void RemoveByType(ItemType type)
    {
        for (int i = 0; i < _itemsMain.Count; i++)
        {
            if (_itemsMain[i] != null && _itemsMain[i].item.Type == type)
            {
                Destroy(_itemsMain[i].item.gameObject);
                _itemsMain[i] = null;
            }
        }
        //_itemsMain.RemoveAll(x => x == null);
        InventoryMainUpdate?.Invoke(_itemsMain);

/*        for (int i = 0; i < _itemsUsable.Count; i++)
        {
            if (_itemsUsable[i] != null && _itemsUsable[i].item.Type == type)
            {
                var inventoryItem = _itemsUsable[i];
                inventoryItem.amount--;
                if (inventoryItem.amount <= 0)
                {
                    Destroy(inventoryItem.item.gameObject);
                    _itemsUsable.);
                    i--;
                }
                else
                {
                    _itemsUsable[i] = inventoryItem;
                }
            }
        }*/
        InventoryUsableUpdate?.Invoke(_itemsUsable);
    }


    public void TrySetActive(int index)
    {
        if (index >= _itemsUsable.Count ||  _itemsUsable[index].item == null || index == _currentActiveIdx) return;

        if (_itemsUsable[index].item.Type == ItemType.Seed)
        {
            if (_currentActiveIdx != -1)
            {
                _itemsUsable[_currentActiveIdx].active = false;
                _itemsUsable[_currentActiveIdx].item.gameObject.SetActive(false);
                _itemsUsable[_currentActiveIdx].item.transform.SetParent(transform);
            }
            _currentActiveIdx = index;

            _itemsUsable[_currentActiveIdx].item.gameObject.SetActive(true);
            _itemsUsable[_currentActiveIdx].item.transform.SetParent(_handPoint);
            _itemsUsable[_currentActiveIdx].item.transform.localPosition = Vector3.zero;
            _itemsUsable[_currentActiveIdx].active = true;

            InventoryUsableUpdate?.Invoke(_itemsUsable);
        }
    }

    public Item GetActiveItem()
    {
        if (_currentActiveIdx != -1 && _itemsUsable[_currentActiveIdx].amount > 0)
        {
            return _itemsUsable[_currentActiveIdx].item;
        }
        return null;
    }
}