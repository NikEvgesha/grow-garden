public class FishItem : Item
{
    private FishInfo _fish;


    private void Awake()
    {
        _fish = GetComponent<FishInfo>();
        type = ItemType.Fish;

    }


    public bool Oncatch()
    {
        //item = fish.Data;
        //weight = fish.Weight;
        return Inventory.Instance.Add(this);
    }


}