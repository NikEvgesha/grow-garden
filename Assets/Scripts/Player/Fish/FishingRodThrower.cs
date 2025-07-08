using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodThrower : MonoBehaviour
{
    [SerializeField] private float _pingPongSpeedMax = 2f;   // скорость пинга
    [SerializeField] private float _pingPongSpeedMin = 0.5f;   // скорость пинга
    [SerializeField] private WaterChecer _floatPrefab;      // префаб поплавка
    [SerializeField] private WaterChecer _activeFloat;      // префаб поплавка
    [SerializeField] private Transform _castPoint;        // точка спавна поплавка
    [SerializeField] private float _minForce = 5f;
    [SerializeField] private float _maxForce = 15f;       // максимальна€ сила броска
    [SerializeField] private float _elevation = 30f;       // ”гол подъЄма в градусах
    [SerializeField] private float _checkTime = 3f;       // ”гол подъЄма в градусах
    [SerializeField] private PowersUI _powersUI;

    [Header("Power")]
    [SerializeField] private List<int> _maxPower = new List<int> { 20, 40, 65, 90, 101 };


    private float _pingPongSpeed = 1f;   // скорость пинга

    private Coroutine _checkWaterCoroutine;

    private float _powerThrow;
    private float _powerThrowTemp = 0f;
    public void PreWindupRod()
    {
        ChangePower();
        if (_powerThrowTemp > 0.2f)
        {
            _pingPongSpeed = Random.Range(_pingPongSpeedMin, _pingPongSpeedMax);
            Fishing.Instance.SetNewState(FishingState.Throw);
        }
    }
    public void WindupRod(PowerFishUI ui)
    {
        ChangePower();
        ui.Power = _powerThrow;
    }
    private void ChangePower()
    {
        _powerThrow = Mathf.PingPong(_powerThrowTemp, 1f);
        _powerThrowTemp += Time.deltaTime * _pingPongSpeed;
    }

    public void ThrowRod()
    {
        ThrowFloat(_powerThrow);
        _powerThrowTemp = 0f;
    }
    void ThrowFloat(float normalizedPower)
    {
        if (!_activeFloat)
            _activeFloat = Instantiate(_floatPrefab, _castPoint.position, _floatPrefab.transform.rotation);

        _activeFloat.transform.position = _castPoint.position;

        Rigidbody rb = _activeFloat.gameObject.GetComponent<Rigidbody>();
        float force = Mathf.Lerp(_minForce, _maxForce, normalizedPower);
        PowerUIChois(normalizedPower * 100);
        Vector3 dir = Quaternion.AngleAxis(_elevation, _castPoint.right) * _castPoint.forward;
        rb.AddForce(dir * force, ForceMode.VelocityChange);
        // здесь можно запустить корутину ожидани€ рыбы
        _activeFloat.StarFish.AddListener(StartFishing);
        _checkWaterCoroutine = StartCoroutine(CheckWater());
    }
    private void StartFishing()
    {
        Fishing.Instance.SetNewState(FishingState.WaitFish);
    }
    private IEnumerator CheckWater()
    {
        yield return new WaitForSeconds(_checkTime);
        
        if (_activeFloat && !_activeFloat.InWater)
        {
            _activeFloat.StarFish.RemoveListener(StartFishing);
            Fishing.Instance.SetNewState(FishingState.NoFishing);
            StartCoroutine(FishGameUI.Instance.ShowNoWater());
        }
    }
    private void PowerUIChois(float power)
    {
        for (int i = 0; i < _maxPower.Count; i++)
        {
            if (power < _maxPower[i]) 
            {
                _powersUI.NewPower(i);
                return;
            }
        }
    }
    public void FinishGame()
    {
        StopCoroutine(_checkWaterCoroutine);
        if (_activeFloat)
            Destroy(_activeFloat.gameObject);
        _activeFloat = null;
    }
    public void PreStop()
    {
        if (_checkWaterCoroutine == null) return;
            StopCoroutine(_checkWaterCoroutine);
        if (!_activeFloat) return;
            _activeFloat.StarFish.RemoveListener(StartFishing);
    }
}
