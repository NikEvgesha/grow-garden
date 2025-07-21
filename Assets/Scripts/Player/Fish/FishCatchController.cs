using System.Collections;
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

    [Header("Region Physics")]
    [SerializeField] private float acceleration = 20f;     // сила, с которой Ђнакапливаетс€ї скорость
    [SerializeField] private float maxSpeed = 6f;      // максимальна€ скорость смещени€
    [SerializeField] private float drag = 5f;      // сопротивление (чем выше Ч тем быстрее тормозитс€)
    [SerializeField] private float bounceDamping = 0.5f;    // сколько скорости остаЄтс€ после отскока (0Е1)


    [Header("Progress Settings")]
    [SerializeField] private float _fillRate = 0.2f;             // Rate of progress change per second
    [SerializeField] private float _initialFill = 0.1f;          // 10% initial fill

    private float _playerSize;
    private float _targetX;
    private float _currentFillRate;     // реальна€ скорость заполнени€ в этой игре
    private float _fishResistance = 1f; // сила рыбы: >1 Ч замедл€ет заливку, <1 Ч ускор€ет

    private float _regionVelocity = 0f;

    private float _lerpT;        // накопленный параметр [0Е1]
    private Vector2 _startPos;   // точка старта при каждом новом движении

    private FishGameUI _fishGameUI;
    bool _playing = false;
    private void Start()
    {
        _fishGameUI = FishGameUI.Instance;
    }
    public void Begin(FishInfo fish)
    {
        _fishResistance = fish.GetStrong();
        _currentFillRate = Mathf.Max(_fillRate / _fishResistance, 0.01f);
        float percent = Mathf.Lerp(_playerSizeMax, _playerSizeMin, fish.GetDifficulty());
        _fishGameUI.SetPlayerAnchoredSize(percent);
        _fishGameUI.Progress = _initialFill;
        _containerSize = _fishGameUI.GetContainerSize();
        _playerSize = _fishGameUI.SizePlayer;

        Vector2 fishCenter = new Vector2(0f, _fishGameUI.FishPosition.y);
        Vector2 playerCenter = new Vector2(0f, _fishGameUI.PlayerPosition.y);

        _fishGameUI.FishPosition = fishCenter;
        _fishGameUI.PlayerPosition = playerCenter;

        PickNewTarget();
        _fishGameUI.StartGame(_initialFill);
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
    public void HandleRegionPhysics(float inputDir = -1)
    {
        if (!_playing) { return; }
        // 1) –ассчитываем направление Ђсилыї
        //inputDir = Input.GetMouseButton(0) ? +1f : -1f;

        // 2) »нтегрируем ускорение
        _regionVelocity += inputDir * acceleration * Time.deltaTime;

        // 3) ƒобавл€ем Ђвоздушногої сопротивлени€ (драг)
        //    сила торможени€ пропорциональна скорости
        float dragForce = drag * _regionVelocity * Time.deltaTime;
        _regionVelocity -= dragForce;

        // 4) ќграничиваем скорость
        _regionVelocity = Mathf.Clamp(_regionVelocity, -maxSpeed, +maxSpeed);
        //Debug.Log(_regionVelocity);
        // 5) ѕеремещаем игрока
        Vector2 pos = _fishGameUI.PlayerPosition;
        pos.x += _regionVelocity * Time.deltaTime;

        // 6) √раницы и отскок
        float halfW = _containerSize * 0.5f;
        float halfR = _playerSize * 0.5f;
        float minX = -halfW + halfR;
        float maxX = +halfW - halfR;

        if (pos.x < minX)
        {
            pos.x = minX;
            _regionVelocity = -_regionVelocity * bounceDamping;
        }
        else if (pos.x > maxX)
        {
            pos.x = maxX;
            _regionVelocity = -_regionVelocity * bounceDamping;
        }

        _fishGameUI.PlayerPosition = pos;
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
        }
        else if (_fishGameUI.Progress >= 1f)
        {
            Debug.Log("You won!");
            OnCatch();
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
        _playing = false;
        _fishGameUI.EndGame();
        Fishing.Instance.SetNewState(FishingState.NoFishing);
    }
    public void FinishGame()
    {
        OnEnd();
    }
}
