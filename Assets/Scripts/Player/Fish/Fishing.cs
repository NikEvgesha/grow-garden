using System;
using UnityEngine;
[RequireComponent(typeof(FishBiteController), typeof(FishCatchController), typeof(FishingRodThrower))]

public class Fishing : MonoBehaviour
{
    public static Fishing Instance;

    [SerializeField] private bool _TestActivate = false;

    public Action<bool> StartFishing;
    public Action<float> PowerWindup;

    private bool _isHolding = false;
    private bool _isStartFishing = false;
    private bool _isFindeFishing = false;

    [Header("Windup and Throw Rod")]
    [SerializeField] private FishingRodThrower _fishingRodThrower;

    [Header("Waiting Fish")]
    [SerializeField] private FishBiteController _fishBiteDelay;

    [Header("FishMiniGame")]
    [SerializeField] private FishCatchController _fishCatchController;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
        _fishBiteDelay = GetComponent<FishBiteController>();
        _fishCatchController = GetComponent<FishCatchController>();
    }
    private void Start()
    {
        RoadActivate(_TestActivate);
    }
    private void RoadActivate(bool isActivate)
    {
        if (isActivate)
        {
            PlayerInput.Instance.AAttack += UseRoad;
            PlayerInput.Instance.AStopAttack += StopUseRoad;
        }
        else
        {
            PlayerInput.Instance.AAttack -= UseRoad;
            PlayerInput.Instance.AStopAttack -= StopUseRoad;
        }
    }
    private void UseRoad()
    {
        if (!_isFindeFishing)
        {
            WindupRod();
        }
        else
        {
            _fishCatchController.HandleRegionInput(1f);
        }

    }
    private void StopUseRoad()
    {
        if (!_isFindeFishing)
        {
            ThrowRod();
        } 
        else
        {
            _fishCatchController.HandleRegionInput(-1f);
        }
    }

    private void WindupRod()
    {
        if (_isStartFishing)
            return;
        if (!_isHolding)
            _isHolding = !_isHolding;
        _fishingRodThrower.WindupRod();
    }

    private void ThrowRod()
    {
        if (!_isHolding)
            return;
        _isHolding = false;
        _isStartFishing = true;
        StartFishing?.Invoke(_isStartFishing);
        _fishingRodThrower.ThrowRod();
    }
    public void StartBiteDelay()
    {
        StartCoroutine(_fishBiteDelay.StartWaiting());
    }
    public void FindFish(Fish fish)
    {
        _isFindeFishing = true;
        _fishCatchController.Begin(fish);
    }
    public void FinishFishing()
    {
        _isFindeFishing = false;
        _isStartFishing = false;
    }
}