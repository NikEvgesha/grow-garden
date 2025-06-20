using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableOBjects", fileName = "Plant")]
public class PlantData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _baseCost;
    [SerializeField] private Plant _prefab;
    [SerializeField] private float _growTimeSeconds;
}
