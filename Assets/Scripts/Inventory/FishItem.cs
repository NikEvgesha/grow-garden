public class FishItem : Item
{
    private Fish fish;


    private void Awake()
    {
        fish = GetComponent<Fish>();
        type = ItemType.Fish;

    }


    public bool Oncatch()
    {
        //item = fish.Data;
        //weight = fish.Weight;
        return Inventory.Instance.Add(this);
    }


}