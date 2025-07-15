using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected ItemData item;
    protected float weight;
    protected ItemType type;

    public ItemType Type => type;
    public float Weight => weight;
    public ItemData Data => item;

}