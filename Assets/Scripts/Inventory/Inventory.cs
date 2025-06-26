using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData item;
    public int amount;
    public float weight; // for Plants
                         //public GameObject prefab;
}


public class Inventory : MonoBehaviour
{


    private static Inventory _instance;
    public static Inventory Instance => _instance;


    [SerializeField] private int _capacity = 20;
    [SerializeField] private Transform _handPoint;


    private List<InventoryItem> _items;

    public int Capacity => _capacity;

    public Action<List<InventoryItem>> InventoryUpdate;

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
        _items = new List<InventoryItem>();
    }


    public bool Add(HarvestablePlant harvestable)
    {
        if (_capacity > _items.Count)
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
    }

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

}
