using UnityEngine;

public class ItemData: ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _maxStackSize;

    public string Name => _name;
    public Sprite Icon => _icon;
    public int MaxStackSize => _maxStackSize;
}