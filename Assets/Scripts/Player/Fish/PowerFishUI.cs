using UnityEngine;
using UnityEngine.UI;

public class PowerFishUI : MonoBehaviour
{
    [SerializeField] private Slider _powerSlider;
    [SerializeField] private GameObject _active;
    private bool _activateUI;
    public bool ActivateUI 
    { 
        get { return _activateUI; }
        set { 
            if (_activateUI != value)
            {
                _activateUI = value;
                _active.SetActive(value);
            }
        }
    }
    public float Power
    {
        set
        {
            _powerSlider.value = value;
        }
    }
}
