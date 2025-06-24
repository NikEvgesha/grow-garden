using System.Collections.Generic;
using UnityEngine;

public class CropSpawnerPlant : Plant
{
    [SerializeField] List<Transform> _cropSpawnPoints;
    [SerializeField] PlantData _cropPlant;

    protected override void OnGrowFinish()
    {
        base.OnGrowFinish();

        foreach (Transform point in _cropSpawnPoints)
        {
            SpawnPlant(point);
        }
    }

    private void SpawnPlant(Transform point)
    {
        Plant plant = PlantsManager.Instance.GetPlantInstance(_cropPlant);
        plant.transform.SetParent(point);
        plant.transform.localPosition = Vector3.zero;

        if (plant is HarvestablePlant)
        {
            ((HarvestablePlant)plant).SetSpawnPoint(point);
            ((HarvestablePlant)plant).OnHarvest += SpawnPlant;
        }

    }

    

}