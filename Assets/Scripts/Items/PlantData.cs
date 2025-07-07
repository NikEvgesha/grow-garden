using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObject/Plant")]
public class PlantData : ItemData
{
    [SerializeField] private bool _harvestable; // true - собирается и исчезает, false - не собирается, но спавнит на себе другой плант
    [SerializeField] private int _baseCost;
    [SerializeField] private float _baseWeight;
    [SerializeField] private Plant _plantPrefab;
    [SerializeField] private float _growTimeSeconds;


    public int BaseCost => _baseCost;
    public float BaseWeight => _baseWeight;
    public Plant Prefab => _plantPrefab;
    public float GrowTime => _growTimeSeconds;

    public bool Harvestable => _harvestable;
}
