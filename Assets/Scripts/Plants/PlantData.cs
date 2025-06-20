using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObject/Plant")]
public class PlantData : ScriptableObject
{
    [SerializeField] private string _name; // он же id для сохранений?
    [SerializeField] private bool _harvestable; // true - собирается и исчезает, false - не собирается, но спавнит на себе другой плант
    [SerializeField] private int _baseCost; // умножается на уровень конкретного растения (устанавливается случайно при инстансе префаба)
    [SerializeField] private float _baseWeight; // в граммах
    //[SerializeField] private int _maxLevel;
    [SerializeField] private Plant _plantPrefab;
    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] private float _growTimeSeconds;
    [SerializeField] private Sprite _plantIcon;
    [SerializeField] private Sprite _seedIcon;
    //[SerializeField] private Sprite _goldPlantIcon;


    public string Name => _name;
    public int BaseCost => _baseCost;
    public float BaseWeight => _baseWeight;
    //public int MaxLevel => _maxLevel;
    public Plant Prefab => _plantPrefab;
    public GameObject Seed => _seedPrefab;
    public float GrowTime => _growTimeSeconds;
    public Sprite PlantIcon => _plantIcon;
    public Sprite SeedIcon => _seedIcon;

    public bool Harvestable => _harvestable;
}
