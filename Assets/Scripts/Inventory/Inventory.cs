using System.Collections.Generic;
using UnityEngine;


/*public struct InventoryItem
{
    public ItemType type;
    public string name;
    public Sprite IMG;
    public float weight; // for Plants
    //public GameObject prefab;
}*/

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _handPoint;


    private List<ItemData> _items;

}
