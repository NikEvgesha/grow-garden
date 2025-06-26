using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlantSaveData
{
    public string id; // чтобы забрать потом нужную PlantData из менеджера
    public int stage; // стадия роста 
    public float weight;
    //... ?
}
[Serializable]
public struct GrowStage
{
    public GameObject obj;
    public bool isFinal;
}


public class Plant : MonoBehaviour
{
    [SerializeField] protected Transform _growStageObjectsParent;
    [SerializeField] protected List<GrowStage> _growStages;

    protected float _weight;
    protected float _weightMultiplier = 1f;
    protected PlantData _plantData;
    protected int _curentStageIdx = 0;
    protected float stageDuration;
    protected bool _isFullyGrown;


    public bool Grown => _isFullyGrown;
    public PlantData Data => _plantData;

    public void Init(PlantData plantData)
    {
        _plantData = plantData;
        _weight = _plantData.BaseWeight * _weightMultiplier;
        _isFullyGrown = false;
        stageDuration = _plantData.GrowTime / _growStages.Count;
        StartCoroutine(Grow());
    }


    public void SetWeightMultiplier(float weightMultiplier)
    {
        _weightMultiplier = weightMultiplier;
        //atransform.localScale = Vector3.one * (_weight / _plantData.BaseWeight / 2f);
        //Debug.Log(_plantData.Name + ": set weight " + Weight);
    }

    protected virtual IEnumerator Grow()
    {
        _growStageObjectsParent.transform.localScale = Vector3.zero;
        while (_curentStageIdx < _growStages.Count)
        {
            _growStages[_curentStageIdx].obj.SetActive(true);
            //if (_growStages[_curentStageIdx].isFinal) break;
            float currentStageTime = 0;
            while (currentStageTime < stageDuration)
            {
                currentStageTime += Time.deltaTime;
                _growStageObjectsParent.transform.localScale = Vector3.one * Mathf.Lerp(0, _weightMultiplier, ((stageDuration * _curentStageIdx) + currentStageTime) / _plantData.GrowTime);
                yield return null;
            }
            if (_growStages[_curentStageIdx].isFinal) break;
            Destroy(_growStages[_curentStageIdx].obj);
            _curentStageIdx++;
        }
        OnGrowFinish();
    }


    protected virtual void OnGrowFinish()
    {
        _isFullyGrown = true;
    }
}
