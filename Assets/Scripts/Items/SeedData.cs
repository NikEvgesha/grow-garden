using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "ScriptableObject/Seed")]
public class SeedData : ItemData
{
    [SerializeField] private int _coinPrice;
    [SerializeField] private int _gemPrice;
    [SerializeField] private PlantData _plantData;
    [SerializeField] private int _marketStack;


    public int CoinPrice => _coinPrice;
    public int GemPrice => _gemPrice;

    public PlantData Plant => _plantData;
    public int MarketStack => _marketStack;
}