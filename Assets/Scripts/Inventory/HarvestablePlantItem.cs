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