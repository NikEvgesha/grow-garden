using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public struct InventoryItem
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


    private List<Item> _itemsUsable;
    private List<Item> _itemsMain;

    //private Dictionary<Item, int> _items;

    public int CapacityUsable => _capacityUsable;
    public int CapacityMain => _capacityMain;

    public Action<List<Item>> InventoryUsableUpdate;
    public Action<List<Item>> InventoryMainUpdate;

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
        _itemsUsable = new List<Item>();
        _itemsMain = new List<Item>();
    }

/*    public bool Add(HarvestablePlant harvestable)
    {
        if (CheckEmptySlot())
        {
            InventoryItem item = new InventoryItem
            {
                item = harvestable.Data,
                amount = 0,
                weight = harvestable.Weight
            };
            _items.Add(item);
            InventoryUpdate?.Invoke(_items);
            return true;
        }
        return false;
    }*/


    public bool Add(Item item)
    {

        bool added = false;
        if ((item is HarvestPlantItem || item is FishItem) && CheckEmptySlot(true))
        {
            _itemsMain.Add(item);
            InventoryMainUpdate?.Invoke(_itemsMain);
            added = true;
        }

        if (item is SeedItem && CheckEmptySlot(false))
        {
            _itemsUsable.Add(item);
            InventoryUsableUpdate?.Invoke(_itemsUsable);
            added = true;
        }

        if (added)
        {
            item.transform.SetParent(_itemsParent);
            item.gameObject.SetActive(false);
        }
        

        return added;
    }

/*public bool Add(SeedData seed) // add(Seed seed) - сразу инстанцировать и добавлять?
    {
        if (CheckEmptySlot())
        {
            InventoryItem item = new InventoryItem
            {
                item = seed,
                amount = 1,
                weight = 0
            };
            _items.Add(item);
            InventoryUpdate?.Invoke(_items);
            return true;
        }
        return false;
    }*/

/*    public bool Add(Fish fish)
    {
        if (_capacity > _items.Count)
        {
            InventoryItem item = new InventoryItem
            {
                item = fish.Data,
                amount = 0,
                weight = fish.Weight
            };
            _items.Add(item);
            InventoryUpdate?.Invoke(_items);
            return true;
        }
        return false;
    }*/

    public bool CheckEmptySlot(bool inMainInventory)
    {

        bool res = inMainInventory ? _capacityMain > _itemsMain.Count : _capacityUsable > _itemsUsable.Count;
        if (!res)
            GameUI.Instance.Hints.ShowHint(UIHintType.NoSpaceInInventory);
        return res;
    }

    public ReadOnlyCollection<Item> GetItemsByType(ItemType type)
    {
        return _itemsMain.FindAll(x => x.Type == type).AsReadOnly();
    }

    public void RemoveByType(ItemType type)
    {
        for (int i = 0; i < _itemsMain.Count; i++)
        {
            if (_itemsMain[i].Type == type)
            {
                Destroy(_itemsMain[i].gameObject);
                _itemsMain[i] = null;
            }
        }
        _itemsMain.RemoveAll(x => x == null);
        InventoryMainUpdate?.Invoke(_itemsMain);
    }

}
