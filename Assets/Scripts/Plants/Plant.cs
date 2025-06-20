using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantData _plantData;
    [SerializeField] private Transform _growStageObjectsParent;

    private List<GameObject> _growStageObjects;

    private void Awake()
    {
        _growStageObjects = _growStageObjectsParent.GetComponentsInChildren<GameObject>().ToList();
    }
}
