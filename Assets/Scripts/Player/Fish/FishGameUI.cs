using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishGameUI : MonoBehaviour
{
    public static FishGameUI Instance;
    [SerializeField] private GameObject _active;

    [Header("Icon Settings")]
    [SerializeField] private RectTransform _fishUI;               // The moving icon
    [SerializeField] private RectTransform _pathContainer;       // Container representing the path (e.g., an empty RectTransform with a horizontal line)

    [Header("Region Settings")]
    [SerializeField] private RectTransform _catchRegion;         // The player's catch area
    [SerializeField] private float _sizeCatchRegion;

    [Header("Progress Settings")]
    [SerializeField] private Image _progressBar;                 // UI Image representing fill amount

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

}
