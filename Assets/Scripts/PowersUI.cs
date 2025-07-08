using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersUI : MonoBehaviour
{
    [SerializeField] private GameObject _powerContainer;
    [SerializeField] private List <GameObject> _powers;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _curve = new AnimationCurve();
    public void NewPower(int power)
    {
        _powerContainer.transform.position = _startPoint.position;
        _powerContainer.SetActive(true);
        for (int i = 0; i < _powers.Count; i++)
        {
            _powers[i].SetActive(power == i);
        }
        StartCoroutine(AnimationPower());
    }
    private IEnumerator AnimationPower()
    {
        float t = 0;
        float tCurve = _curve.Evaluate(t);
        while ((_powerContainer.transform.position - _endPoint.position).magnitude > 0)
        {
            yield return null;
            t += Time.deltaTime * _speed;
            tCurve = _curve.Evaluate(t);
            _powerContainer.transform.position = Vector3.Lerp(_startPoint.position, _endPoint.position, tCurve);
        }
        _powerContainer.SetActive(false);
    }
}
