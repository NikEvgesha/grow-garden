using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public struct InventoryItem
{
    public ItemData item;
    public int amount;
    public float weight; // for Plants
    public bool active;
}



public abstract class Item : MonoBehaviour
{
    protected ItemData item;
    protected float weight;
    protected ItemType type;

    public ItemType Type => type;
    public float Weight => weight;
    public ItemData Data => item;

}

public class HarvestPlantItem : Item
{
    private HarvestablePlant plant;


    private void Awake()
    {
        plant = GetComponent<HarvestablePlant>();
        type = ItemType.Plant;
    }


    public bool TryHarvest()
    {
        item = plant.Data;
        weight = plant.Weight;

        return Inventory.Instance.Add(this);
    }
}

public class SeedItem : Item
{
    private Seed seed;


    private void Awake()
    {
        seed = GetComponent<Seed>();
        type = ItemType.Seed;
        weight = 0;
    }

}




public class Inventory : MonoBehaviour
{


    private static Inventory _instance;
    public static Inventory Instance => _instance;


    [SerializeField] private int _capacity = 20;
    [SerializeField] private Transform _handPoint;


    private List<Item> _items;

    public int Capacity => _capacity;

    public Action<List<Item>> InventoryUpdate;

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
        _items = new List<Item>();
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
        if (CheckEmptySlot())
        {
            _items.Add(item);
            InventoryUpdate?.Invoke(_items);
            return true;
        }
        return false;
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

    public bool CheckEmptySlot()
    {
        bool res = _capacity > _items.Count;
        if (!res)
            GameUI.Instance.Hints.ShowHint(UIHintType.NoSpaceInInventory);
        return res;
    }

    public ReadOnlyCollection<Item> GetItemsByType(ItemType type)
    {
        return _items.FindAll(x => x.Type == type).AsReadOnly();
    }

    public void RemoveByType(ItemType type)
    {
        _items.RemoveAll(x => x.Type == type);
        InventoryUpdate?.Invoke(_items);
    }

}
