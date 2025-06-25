using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FishCatchController : MonoBehaviour
{

    [Header("Icon Settings")]
    [SerializeField] private float _containerSize;
    [SerializeField] private float _fishSpeed = 1f;                // „ем выше Ч тем быстрее Ђдоезжаетї
    [SerializeField] private AnimationCurve _moveCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Region Settings")]
    [SerializeField] private float _playerSizeMin = 0.1f;
    [SerializeField] private float _playerSizeMax = 0.4f;
    [SerializeField] private float _regionSpeed = 5f;            // Speed of region movement

    [Header("Progress Settings")]
    [SerializeField] private float _fillRate = 0.2f;             // Rate of progress change per second
    [SerializeField] private float _initialFill = 0.1f;          // 10% initial fill

    private float _playerSize;
    private float _targetX;
    private float _currentFillRate;     // реальна€ скорость заполнени€ в этой игре
    private float _fishResistance = 1f; // сила рыбы: >1 Ч замедл€ет заливку, <1 Ч ускор€ет

    private float _lerpT;        // накопленный параметр [0Е1]
    private Vector2 _startPos;   // точка старта при каждом новом движении

    private FishGameUI _fishGameUI;
    bool _playing = false;
    private void Start()
    {
        _fishGameUI = FishGameUI.Instance;
    }
    public void Begin(Fish fish)
    {
        _fishResistance = fish.GetStrong();
        _currentFillRate = Mathf.Max(_fillRate / _fishResistance, 0.01f);
        float percent = Mathf.Lerp(_playerSizeMax, _playerSizeMin, fish.GetDifficulty());
        _fishGameUI.SetPlayerAnchoredSize(percent);
        _fishGameUI.Progress = _initialFill;
        _containerSize = _fishGameUI.GetContainerSize();
        _playerSize = _fishGameUI.SizePlayer;
        PickNewTarget();
        _fishGameUI.StartGame();
        StartCoroutine(StartGame());

    }
    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        _playing = true;
    }
    void Update()
    {
        if (!_playing) { return; }
        MoveIcon();
        //HandleRegionInput();
        UpdateProgress();
        CheckEndConditions();
    }

    void PickNewTarget()
    {
        _startPos = _fishGameUI.FishPosition;
        // Random x within path bounds
        float halfWidth = _containerSize * 0.5f;
        _targetX = Random.Range(-halfWidth, halfWidth);

        _lerpT = 0f;
    }

    void MoveIcon()
    {
        
        // наращиваем t
        _lerpT += Time.deltaTime * (_fishSpeed/10);
        float tClamped = Mathf.Clamp01(_lerpT);

        // примен€ем кривую (по умолчанию линейна€)
        float curvedT = _moveCurve.Evaluate(tClamped);

        // считаем новую позицию
        Vector2 targetPos = new Vector2(_targetX, _startPos.y);
        _fishGameUI.FishPosition = Vector2.Lerp(_startPos, targetPos, curvedT);

        // если дошли до конца (t == 1), берем новый
        if (tClamped >= 1f)
            PickNewTarget();
    }

    public void HandleRegionInput(float direction = -1)
    {
        if (!_playing) { return; }
        // Right mouse button held moves region right, otherwise moves left
        //direction = Input.GetMouseButton(0) ? 1f : -1f;
        Vector2 rPos = _fishGameUI.PlayerPosition;
        rPos.x += direction * _regionSpeed;

        // Clamp within path bounds
        float halfWidth = _containerSize * 0.5f;
        float halfRegion = _playerSize * 0.5f;
        rPos.x = Mathf.Clamp(rPos.x, -halfWidth + halfRegion, halfWidth - halfRegion);
        _fishGameUI.PlayerPosition = rPos;
    }

    void UpdateProgress()
    {
        // Check if icon is within region
        float iconX = _fishGameUI.FishPosition.x;
        float regionLeft = _fishGameUI.PlayerPosition.x - _playerSize * 0.5f;
        float regionRight = _fishGameUI.PlayerPosition.x + _playerSize * 0.5f;

        if (iconX >= regionLeft && iconX <= regionRight)
        {
            _fishGameUI.Progress += _currentFillRate * Time.deltaTime;
        }
        else
        {
            _fishGameUI.Progress -= _fillRate * Time.deltaTime;
        }

        // Clamp
        _fishGameUI.Progress = Mathf.Clamp01(_fishGameUI.Progress);
    }

    void CheckEndConditions()
    {
        if (_fishGameUI.Progress <= 0f)
        {
            Debug.Log("You lost!");
            OnLose();
            _playing = false;
        }
        else if (_fishGameUI.Progress >= 1f)
        {
            Debug.Log("You won!");
            OnCatch();
            _playing = false;
        }
    }

    void OnCatch()
    {
        //playing = false;
        Debug.Log("–ыба поймана!");
        // тут награда, звук и т.п.
        OnEnd();
    }

    void OnLose()
    {
        //playing = false;
        Debug.Log("”пустили рыбу!");
        // можно отдать меньше награды или отправить в состо€ние Ђждать клевї
        OnEnd();
    }
    void OnEnd()
    {
        _fishGameUI.EndGame();
        Fishing.Instance.SetNewState(FishingState.NoFishing);
    }
}
