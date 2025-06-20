using UnityEngine;

public class PlantsManager : MonoBehaviour
{
    private static PlantsManager _instance;
    public static PlantsManager Instance => _instance;


    [SerializeField] private float _maxWeightMiltiplier;


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
            harvestable.Init(plantData);
            harvestable.SetWeight(GetRandomWeight(plantData.BaseWeight));
        }
        else
        {
            CropSpawnerPlant harvestable = plant.GetComponent<CropSpawnerPlant>();
            harvestable.Init(plantData);
        }
            

        return plant;
    }



    private float GetRandomWeight(float baseWeight)
    {
        float multiplier = Random.Range(1.0f, _maxWeightMiltiplier);
        float weight = Mathf.Round(baseWeight * multiplier * 100f) / 100f; // округляем до 2 знаков после запятой
        return weight;
    }

}