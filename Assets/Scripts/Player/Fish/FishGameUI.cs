using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishGameUI : MonoBehaviour
{
    public static FishGameUI Instance;
    [SerializeField] private GameObject _active;

    [Header("Icon Settings")]
    [SerializeField] private RectTransform _fishUI;               // The moving icon
    [SerializeField] private RectTransform _pathContainer;       // Container representing the path (e.g., an empty RectTransform with a horizontal line)
    [SerializeField] private Image _fishIconEnable;                
    [SerializeField] private Image _fishIconDisable;                

    [Header("Region Settings")]
    [SerializeField] private RectTransform _catchRegion;         // The player's catch area
    [SerializeField] private float _sizeCatchRegion;

    [Header("Progress Settings")]
    [SerializeField] private Image _progressBar;                 // UI Image representing fill amount

    [Header("Show No Water")]
    [SerializeField] private GameObject _noWater;

    private bool _activateRoad;

    private float _progress;
    public float Progress
    {
        get { return _progress; }
        set 
        { 
            _progress = value;
            _progressBar.fillAmount = _progress;
        }
    }
    public float SizePlayer
    {
        get { return _catchRegion.rect.width; }
        set 
        {
            _sizeCatchRegion = value;
            //_catchRegion.rect.width = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }
    public void StartGame()
    {
        _active.SetActive(true);
    }
    public float GetContainerSize()
    {
        return _pathContainer.rect.width;
    }

    public Vector2 FishPosition 
    {
        get { return _fishUI.anchoredPosition; }
        set
        {
            _fishUI.anchoredPosition = value;
        }
    }
    public Vector2 PlayerPosition
    {
        get { return _catchRegion.anchoredPosition; }
        set
        {
            _catchRegion.anchoredPosition = value;
        }
    }
    public void EndGame()
    {
        _active.SetActive(false);
    }
    public void SetPlayerAnchoredSize(float percent)
    {
        // Задаём горизонтальные якоря так, чтобы элемент занимал 'percent' ширины контейнера
        float half = percent / 2f;

        // Предположим, вы хотите, чтобы центр игрока совпадал с центром контейнера:
        _catchRegion.anchorMin = new Vector2(0.5f - half, _catchRegion.anchorMin.y);
        _catchRegion.anchorMax = new Vector2(0.5f + half, _catchRegion.anchorMax.y);

        // Обнуляем sizeDelta, чтобы ширина считалась строго по анкерам
        _catchRegion.sizeDelta = new Vector2(0, _catchRegion.sizeDelta.y);
    }
    public void _ToggleActivateRoad()
    {
        _activateRoad = !_activateRoad;
        _fishIconEnable.gameObject.SetActive(_activateRoad);
        _fishIconDisable.gameObject.SetActive(!_activateRoad);
        Fishing.Instance.RoadActivate(_activateRoad);
    }
    public IEnumerator ShowNoWater()
    {
        _noWater.SetActive(true);
        yield return new WaitForSeconds(1);
        _noWater.SetActive(false);
    }
}
