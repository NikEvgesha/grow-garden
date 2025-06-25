using UnityEngine;

public class FishingRodThrower : MonoBehaviour
{

    [SerializeField] private bool _TestActivate = false;
    [SerializeField] private float _pingPongSpeed = 1f;   // скорость пинга
    [SerializeField] private Transform _floatPrefab;      // префаб поплавка
    [SerializeField] private Transform _castPoint;        // точка спавна поплавка
    [SerializeField] private float _maxForce = 15f;       // максимальна€ сила броска
    [SerializeField] private float _elevation = 30f;       // ”гол подъЄма в градусах

    private float _powerThrow;
    private float _powerThrowTemp = 0f;
    private void Awake()
    {
        _floatPrefab = Instantiate(_floatPrefab);
    }
    public void WindupRod()
    {
        _powerThrow = Mathf.PingPong(_powerThrowTemp, 1f);
        _powerThrowTemp += Time.deltaTime * _pingPongSpeed;
        Fishing.Instance.PowerWindup?.Invoke(_powerThrow);
    }

    public void ThrowRod()
    {
        ThrowFloat(_powerThrow);
        _powerThrowTemp = 0f;
    }
    void ThrowFloat(float normalizedPower)
    {
        _floatPrefab.position = _castPoint.position;
        _floatPrefab.gameObject.SetActive(true);

        Rigidbody rb = _floatPrefab.gameObject.GetComponent<Rigidbody>();

        Vector3 dir = Quaternion.AngleAxis(_elevation, _castPoint.right) * _castPoint.forward;

        rb.AddForce(dir * (normalizedPower * _maxForce), ForceMode.VelocityChange);
        // здесь можно запустить корутину ожидани€ рыбы

        Fishing.Instance.StartBiteDelay();

    }
}
