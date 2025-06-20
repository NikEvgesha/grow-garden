//using MirraGames.SDK;
using System;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class LoadingManager : MonoBehaviour
{
    [SerializeField] private string _lobbyScene;
    [SerializeField] private string _gameScene;

    private static LoadingManager _instance;
    public static LoadingManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("LoadingManager уже существует! Удаляем дубликат.");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GameLoader.Instance.OnSceneLoaded += OnSceneLoaded;
/*        MirraSDK.WaitForProviders(static () => {
            LoadingManager.Instance.StartGame();
            // Методы SDK не должны вызывать вылет или NullReferenceException,
            // делегат будет вызван только когда все провайдеры имеют статус IsInitialized.
        });*/

    }
    private void StartGame()
    {
        if (SaveManager.Instance.IsNewPlayer)
        {
            
        }
        else
        {

        }
    }

    private void OnDisable()
    {
        GameLoader.Instance.OnSceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded()
    {
        //CurrentLocation = _location;
        //LocationChanged?.Invoke(CurrentLocation);
    }


}
