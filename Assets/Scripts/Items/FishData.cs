
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObject/Fish")]
public class FishData : ItemData
{
    [SerializeField] private int _baseCost;
    [SerializeField] private float _baseWeight;
    [SerializeField] private float _growTimeSeconds;

    [SerializeField] private Fish _fishPrefab;
    [SerializeField] private float _strong = 1;
    [SerializeField] private float _difficulty = 0;
    [SerializeField] private float _chance;
    [SerializeField] private float _weightMin;
    [SerializeField] private float _weightMax;
    [SerializeField] private float _priceMultiply;

    public int BaseCost => _baseCost;
    public float BaseWeight => _baseWeight;
    public float GrowTime => _growTimeSeconds;
    public Fish FishPrefab => _fishPrefab;
    public float Strong => _strong;
    public float Difficulty => _difficulty;
    public float Chance => _chance;
    public float WeightMin => _weightMin;
    public float WeightMax => _weightMax;
    public float PriceMultiply => _priceMultiply;


}
