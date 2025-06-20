using System;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } private set { } }


    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private GameObject _startQuest;
    public Player Player { get { return _player; } }

    public bool isEndGame = false;

    public Action GameStart;
    public Action<int> GameResume;


    private bool _pause;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            isEndGame = false;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        isEndGame = true;
    }
    private void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            //PlayerMovement.Instance.Teleport(_playerSpawnPoint);
        }

        GameStart?.Invoke();

        //StartQuest();
    }

    
    private void StartQuest()
    {
        if (_startQuest != null)
            QuestManager.Instance.StartQuest();
        else
            Debug.LogWarning("Не найден prefab Quest_Kill5Rats_Prefab");
    }

    
}
