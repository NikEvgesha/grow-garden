using System;
using System.Collections.Generic;
using UnityEngine;

public class SeedMarket : InteractablePoint
{
    public struct MarketItemData
    {
        public SeedData data;
        public SeedMarketSlot slot;
        public int amount;
    }

    [SerializeField] private List<SeedData> _seeds;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private SeedMarketSlot _slotPrefab;

    private Dictionary<SeedData, MarketItemData> _market;

    private void Awake()
    {
        _market = new Dictionary<SeedData, MarketItemData>();
    }

    private new void Start()
    {
        base.Start();
        foreach (SeedData seed in _seeds)
        {
            SeedMarketSlot slot = Instantiate(_slotPrefab, _slotsParent);
            slot.Init(this, seed);
            _market.Add(seed, new MarketItemData { data = seed, slot = slot, amount = seed.MarketStack });
        }
    }

    private void RefreshMarket()
    {
        foreach (SeedData seed in _seeds)
        {
            MarketItemData item = _market[seed];
            item.slot.SetAvailability(true);
            item.slot.SetAmount(seed.MarketStack);
        }
    }


    public void TryBuy(SeedData seed, bool forGems)
    {
        if (!Inventory.Instance.CheckEmptySlot()) return;

        bool bought = CurrencyManager.Instance.RemoveCurrency(
            type: forGems ? CurrencyType.Gems : CurrencyType.Coins,
            amount: forGems ? seed.GemPrice : seed.CoinPrice
        );

        if (bought)
        {
            //Inventory.Instance.Add(seed);
            MarketItemData item = _market[seed];
            item.amount -= 1;
            item.slot.SetAmount(item.amount);
            if (item.amount == 0)
            {
                item.slot.SetAvailability(false);
            }
            _market[seed] = item;
        }


    }

}
