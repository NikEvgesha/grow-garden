using System.Collections;
using UnityEngine;

public class FishBiteController : MonoBehaviour
{
    [SerializeField] private float _baseWaitTime = 10f;      // базовое врем€ до клева
    [SerializeField] private int _clickRevard = 1;
    [SerializeField] private FishInfo _defaultFish;

    private float _remainingTime;
    private FishInfo _fish;
    public IEnumerator StartWaiting()
    {
        _remainingTime = _baseWaitTime;
        // показываем на экране кружок Ч пусть это ваш UI-элемент

        while (_remainingTime > 0f)
        {
            // клик по кружку
            if (Input.GetMouseButtonDown(0) && IsPointerOverFloat())
            {
                //_remainingTime = Mathf.Max(0f, _remainingTime - _clickRevard);
            }
            _remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (!_fish)
            _fish = _defaultFish;
        // клев!
        Fishing.Instance.FindFish(_fish);
        //StartReelMinigame();
    }

    // заглушка: проверка, что клик именно по кругу
    bool IsPointerOverFloat()
    {
        // можно проверить ScreenPointToRay и дистанцию до floatPos
        return true;
    }
}
