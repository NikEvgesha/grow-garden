using UnityEngine;

public class ItemData: ScriptableObject
{
    [SerializeField] protected ItemType _type;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _name;
    [SerializeField] protected int _maxStackSize;
    [SerializeField] protected GameObject _prefab;

    public ItemType Type => _type;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int MaxStackSize => _maxStackSize;
    public GameObject Prefab => _prefab;
}