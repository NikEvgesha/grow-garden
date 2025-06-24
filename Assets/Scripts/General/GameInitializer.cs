using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private static GameInitializer _instance;
    public static GameInitializer Instance => _instance;

    [SerializeField] private Player _playerPrefab;


    private Player _player;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _player = Instantiate(_playerPrefab);
    }
}