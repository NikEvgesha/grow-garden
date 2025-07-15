public class SeedItem : Item
{
    private Seed seed;


    private void Awake()
    {
        seed = GetComponent<Seed>();
        type = ItemType.Seed;
        weight = 0;
        
    }


    public bool TryBuy()
    {
        item = seed.Data;
        return Inventory.Instance.Add(this);
    }


}