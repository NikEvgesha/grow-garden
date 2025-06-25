using UnityEngine;
using UnityEngine.UI;

public class FishCatchController : MonoBehaviour
{

    [Header("Icon Settings")]
    [SerializeField] private float _containerSize;
    [SerializeField] private float _fishSpeed = 200f;            // Speed at which icon moves to target

    [Header("Region Settings")]
    [SerializeField] private float _playerSize;
    [SerializeField] private float _regionSpeed = 5f;            // Speed of region movement

    [Header("Progress Settings")]
    [SerializeField] private float _fillRate = 0.2f;             // Rate of progress change per second
    [SerializeField] private float _initialFill = 0.1f;          // 10% initial fill

    private float _targetX;

    private FishGameUI _fishGameUI;
    bool playing = false;
    private void Start()
    {
        _fishGameUI = FishGameUI.Instance;
    }
    public void Begin(Fish fish)
    {
        _fishGameUI.Progress = _initialFill;
        _containerSize = _fishGameUI.GetContainerSize();
        _playerSize = _fishGameUI.SizePlayer;
        PickNewTarget();
        playing = true;
        _fishGameUI.StartGame();
    }
    void Update()
    {
        if (!playing) { return; }
        MoveIcon();
        HandleRegionInput();
        UpdateProgress();
        CheckEndConditions();
    }

    void PickNewTarget()
    {
        // Random x within path bounds
        float halfWidth = _containerSize * 0.5f;
        _targetX = Random.Range(-halfWidth, halfWidth);
    }

    void MoveIcon()
    {
        
        Vector2 pos = _fishGameUI.FishPosition;
        Vector2 targetPos = new Vector2(_targetX, pos.y);
        _fishGameUI.FishPosition = Vector2.MoveTowards(pos, targetPos, _fishSpeed * Time.deltaTime);

        // If reached target, pick a new one
        if (Mathf.Approximately(_fishGameUI.FishPosition.x, _targetX))
        {
            PickNewTarget();
        }
    }

    public void HandleRegionInput(float direction = -1)
    {
        // Right mouse button held moves region right, otherwise moves left
        direction = Input.GetMouseButton(0) ? 1f : -1f;
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
            _fishGameUI.Progress += _fillRate * Time.deltaTime;
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
            playing = false;
        }
        else if (_fishGameUI.Progress >= 1f)
        {
            Debug.Log("You won!");
            OnCatch();
            playing = false;
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
        Fishing.Instance.FinishFishing();
    }
}
