using UnityEngine;

public class FishingRodThrower : MonoBehaviour
{
    [SerializeField] private float _pingPongSpeed = 1f;   // скорость пинга
    [SerializeField] private Transform _floatPrefab;      // префаб поплавка
    [SerializeField] private Transform _activeFloat;      // префаб поплавка
    [SerializeField] private Transform _castPoint;        // точка спавна поплавка
    [SerializeField] private float _minForce = 5f;
    [SerializeField] private float _maxForce = 15f;       // максимальна€ сила броска
    [SerializeField] private float _elevation = 30f;       // ”гол подъЄма в градусах

    private float _powerThrow;
    private float _powerThrowTemp = 0f;
    public void PreWindupRod()
    {
        ChangePower();
        if (_powerThrowTemp > 0.2f)
        {
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
            _activeFloat = Instantiate(_floatPrefab, _castPoint.position, _floatPrefab.rotation);

        _activeFloat.position = _castPoint.position;

        Rigidbody rb = _activeFloat.gameObject.GetComponent<Rigidbody>();
        float force = Mathf.Lerp(_minForce, _maxForce, normalizedPower);
        Vector3 dir = Quaternion.AngleAxis(_elevation, _castPoint.right) * _castPoint.forward;
        rb.AddForce(dir * force, ForceMode.VelocityChange);
        // здесь можно запустить корутину ожидани€ рыбы

        Fishing.Instance.SetNewState(FishingState.WaitFish);
    }
    public void FinishGame()
    {
        if (_activeFloat)
            Destroy(_activeFloat.gameObject);
        _activeFloat = null;
    }
}
