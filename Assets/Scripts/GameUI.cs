using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static GameUI _instance;
    public static GameUI Instance { get { return _instance; } private set { } }

    [SerializeField] private GameplayHints _hints;
    [SerializeField] private InventoryUI _inventory;
    [SerializeField] private SettingUI _settings;

    public GameplayHints Hints => _hints;
    public InventoryUI Inventory => _inventory;
    public SettingUI Settings => _settings;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}