using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    [SerializeField] private GameObject _leftArrow;        
    [SerializeField] private GameObject _rightArrow;
    [SerializeField] private Image _clickImage;
    [SerializeField] private float _alphaClick = 0.4f;


    [Header("Progress Settings")]
    [SerializeField] private Image _progressBar;                 // UI Image representing fill amount
    [SerializeField] private List<Image> _ProgressImage;
    [SerializeField] private Color _colorBad = Color.red;
    [SerializeField] private Color _colorGood = Color.green;


    [Header("Show No Water")]
    [SerializeField] private GameObject _noWater;


    private bool _isLeft;
    private bool _activateRoad;

    private float _progress;
    public float Progress
    {
        get { return _progress; }
        set 
        { 
            float old = _progress;
            bool active = old - value == 0 ? _ProgressImage[0].gameObject.activeSelf : old - value > 0;
            _progress = value;
            _progressBar.fillAmount = _progress;
            if (_ProgressImage.Count == 0) return; 
            foreach (Image image in _ProgressImage)
            {
                image.color = Color.Lerp(_colorBad, _colorGood, _progress);
            }
            if (_ProgressImage[0].gameObject.activeSelf != active)
                _ProgressImage[0].gameObject.SetActive(active);

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
    public void StartGame(float initialFill)
    {
        _progress = initialFill;
        _ProgressImage[0].gameObject.SetActive(false);
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
            float oldX = _catchRegion.anchoredPosition.x;
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
    public void SetForward(bool left)
    {
        if (_isLeft == left) return;
        _isLeft = left;
        _leftArrow.SetActive(left);
        _rightArrow.SetActive(!left);
        float alpha = left ? 1 : _alphaClick; 
        _clickImage.color = new Color(_clickImage.color.r, _clickImage.color.g, _clickImage.color.b, alpha);
    }
}
