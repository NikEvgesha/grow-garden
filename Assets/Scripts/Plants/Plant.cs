using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct PlantSaveData
{
    public string id; // чтобы забрать потом нужную PlantData из менеджера
    public int stage; // стадия роста 
    public float weight;
    //... ?
}


public class Plant : MonoBehaviour
{
    [SerializeField] protected Transform _growStageObjectsParent;

    protected PlantData _plantData;

    protected List<GameObject> _growStageObjects;

    protected void Awake()
    {
        //_growStageObjects = _growStageObjectsParent.GetComponentsInChildren<GameObject>().ToList();
    }


    public void Init(PlantData plantData)
    {
        _plantData = plantData;
    }
}
