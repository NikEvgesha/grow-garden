using System.Collections;
using UnityEngine;

public class Bamboo : HarvestablePlant
{
    [SerializeField] private GameObject _plantSegment;
    [SerializeField] private int _maxSegments = 20;
    [SerializeField] private Transform _startPoint;

    private int _segmentsCount = 0;
    private Transform _nextSegmentPoint;
    private float _currentTime;
    private GameObject _currentSegment;
    private float _totalWeight;

    protected override void OnGrowFinish()
    {
        base.OnGrowFinish();
        _segmentsCount = 1;
        StartCoroutine(SegmentsGrow());
    }
    protected IEnumerator SegmentsGrow()
    {
        _nextSegmentPoint = _startPoint;
        transform.localScale = Vector3.one * _weightMultiplier;
        while (_segmentsCount < _maxSegments)
        {
            _currentSegment = Instantiate(_plantSegment, _growStageObjectsParent);
            _currentSegment.transform.position = _startPoint.position + _nextSegmentPoint.position * (_segmentsCount-1) * _weightMultiplier;
            _nextSegmentPoint = _plantSegment.transform.GetChild(0);
            _currentTime = 0;
            while (_currentTime < _plantData.GrowTime)
            {
                _currentTime += Time.deltaTime;
                _currentSegment.transform.localScale = new Vector3(1, Mathf.Lerp(0, 1, (_currentTime / _plantData.GrowTime)), 1);
                yield return null;
            }
            _segmentsCount++;
        }
    }


    protected override void TryHarvest()
    {
        _totalWeight = _weightMultiplier * _plantData.BaseWeight * _segmentsCount;
        // TODO: collect yo Inventorya
        Destroy(gameObject);
    }
}