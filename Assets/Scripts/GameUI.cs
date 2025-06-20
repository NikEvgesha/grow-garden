using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static GameUI _instance;
    public static GameUI Instance { get { return _instance; } private set { } }

    [SerializeField] private GameplayHints _hints;

    public GameplayHints Hints => _hints;


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