using UnityEngine;
using UnityEngine.Events;

public class WaterChecer : MonoBehaviour
{
    public UnityEvent StarFish = new UnityEvent();
    public bool InWater;
    private void OnTriggerEnter(Collider other)
    {
        if (InWater) return;
        
        if(other.gameObject.tag == "Water")
        {
            InWater = true;
            StarFish.Invoke();
            // анимация ловли
        }
    }
}
