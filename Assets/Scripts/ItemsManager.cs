using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private static ItemsManager _instance;
    public static ItemsManager Instance { get { return _instance; } private set { } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

}
