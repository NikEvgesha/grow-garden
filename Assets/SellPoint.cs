using System.Collections.ObjectModel;
using UnityEngine;

public class SellPoint : InteractablePoint
{
    

    public void SellAll()
    {
        ReadOnlyCollection<InventoryItem> items = Inventory.Instance.GetItemsByType(ItemType.Plant);

        int totalProfit = 0;

        foreach (InventoryItem item in items)
        {
            if (item.item.Type is ItemType.Plant)
            {
                PlantData plantData = (PlantData)item.item.Data;
                totalProfit += (int)Mathf.Round(plantData.BaseCost * item.item.GetComponent<HarvestablePlant>().Weight / plantData.BaseWeight);
            }        
        }

        CurrencyManager.Instance.AddCurrency(CurrencyType.Coins, totalProfit);
        Inventory.Instance.RemoveByType(ItemType.Plant);


    }
}
