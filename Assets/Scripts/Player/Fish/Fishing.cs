using System;
using UnityEngine;
[RequireComponent(typeof(FishBiteController), typeof(FishCatchController), typeof(FishingRodThrower))]

public class Fishing : MonoBehaviour
{
    public static Fishing Instance;

    [SerializeField] private bool _TestActivate = false;

    public Action<bool> StartFishing;
    public Action<float> PowerWindup;

    [Header("Windup and Throw Rod")]
    [SerializeField] private FishingRodThrower _fishingRodThrower;
    [SerializeField] private PowerFishUI _powerFishUI;

    [Header("Waiting Fish")]
    [SerializeField] private FishBiteController _fishBiteDelay;

    [Header("FishMiniGame")]
    [SerializeField] private FishCatchController _fishCatchController;

    private FishingState _fishingState;
    private Coroutine _waitFishCoroutine;
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
        _powerFishUI = GetComponentInChildren<PowerFishUI>();
    }
    private void Start()
    {
        RoadActivate(_TestActivate);
    }
    public void SetNewState(FishingState newState)
    {
        _fishingState = newState;
        switch (_fishingState)
        {
            case FishingState.NoFishing:
                _fishingRodThrower.FinishGame();
                break;
            case FishingState.Throw:
                _powerFishUI.ActivateUI = true;
                break;
            case FishingState.WaitFish:
                _powerFishUI.ActivateUI = false;
                _waitFishCoroutine = StartCoroutine(_fishBiteDelay.StartWaiting());
                break;
            case FishingState.MiniGame:
                break;
            default: break;
        }
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
        switch (_fishingState)
        {
            case FishingState.NoFishing:
                _fishingRodThrower.PreWindupRod();
                break;
            case FishingState.Throw:
                _fishingRodThrower.WindupRod(_powerFishUI);
                break;
            case FishingState.WaitFish:
                SetNewState(FishingState.NoFishing);
                StopCoroutine(_waitFishCoroutine);
                break;
            case FishingState.MiniGame:
                _fishCatchController.HandleRegionInput(1f);
                break;
            default: break;
        }
    }
    private void StopUseRoad()
    {
        switch (_fishingState)
        {
            case FishingState.NoFishing:
                break;
            case FishingState.Throw:
                _fishingRodThrower.ThrowRod();
                break;
            case FishingState.WaitFish:
                break;
            case FishingState.MiniGame:
                _fishCatchController.HandleRegionInput(-1f);
                break;
            default: break;
        }
    }
    public void FindFish(Fish fish)
    {
        SetNewState(FishingState.MiniGame);
        _fishCatchController.Begin(fish);
    }
}