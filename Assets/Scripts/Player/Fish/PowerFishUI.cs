using UnityEngine;
using UnityEngine.UI;

public class PowerFishUI : MonoBehaviour
{
    [SerializeField] private Slider _powerSlider;
    [SerializeField] private GameObject _active;
    private bool isActive;

    private void Start()
    {
        Fishing.Instance.PowerWindup += SetPower;
        Fishing.Instance.StartFishing += DisableUI;
    }
    private void OnDisable()
    {
        Fishing.Instance.PowerWindup -= SetPower;
        Fishing.Instance.StartFishing -= DisableUI;
    }
    private void SetPower(float power)
    {
        if (!isActive)
        {
            isActive = !isActive;
            SetPowerActive(isActive);
        }
        _powerSlider.value = power;
    }
    private void SetPowerActive(bool isActive)
    {
        _active.SetActive(isActive);
    }
    private void DisableUI(bool start)
    {
        if (start)
        {
            isActive = !start;
            _powerSlider.value = 0;
            SetPowerActive(isActive);
        }

    }
}
