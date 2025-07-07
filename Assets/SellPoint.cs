using System.Collections.ObjectModel;
using UnityEngine;

public class SellPoint : InteractablePoint
{
    

    public void SellAll()
    {
        ReadOnlyCollection<Item> items = Inventory.Instance.GetItemsByType(ItemType.Plant);

        int totalProfit = 0;

        foreach (Item item in items)
        {
            if (item.Type is ItemType.Plant)
            {
                PlantData plantData = (PlantData)item.Data;
                totalProfit += (int)Mathf.Round(plantData.BaseCost * item.GetComponent<HarvestablePlant>().Weight / plantData.BaseWeight);
            }        
        }

        CurrencyManager.Instance.AddCurrency(CurrencyType.Coins, totalProfit);
        Inventory.Instance.RemoveByType(ItemType.Plant);


    }
}
