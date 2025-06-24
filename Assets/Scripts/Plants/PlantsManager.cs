using UnityEngine;

public class PlantsManager : MonoBehaviour
{
    private static PlantsManager _instance;
    public static PlantsManager Instance => _instance;


    [SerializeField] private float _maxWeightMiltiplier;
    [SerializeField] private AnimationCurve _weightMultiplierChance;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else
        {
            _instance = this;
        }
    }

    public Plant GetPlantInstance(PlantData plantData)
    {
        Plant plant = Instantiate(plantData.Prefab);
        if (plantData.Harvestable)
        {
            HarvestablePlant harvestable = plant.GetComponent<HarvestablePlant>();
            harvestable.SetWeightMultiplier(GetRandomWeightMultiplier());
            harvestable.Init(plantData);
        }
        else
        {
            CropSpawnerPlant harvestable = plant.GetComponent<CropSpawnerPlant>();
            harvestable.Init(plantData);
        }
            

        return plant;
    }



    /*    private float GetRandomWeight(float baseWeight)
        {
            float random = Random.value;
            float multiplier = Mathf.Lerp(1.0f, _maxWeightMiltiplier, 1.0f - Mathf.Sqrt(random));
            return Mathf.Round(multiplier * 100f) / 100f;
        }*/

    private float GetRandomWeightMultiplier()
    {
        float random = Random.value;
        float multiplier = Mathf.Lerp(1.0f, _maxWeightMiltiplier, 1.0f - Mathf.Sqrt(random));
        return multiplier;
    }

}