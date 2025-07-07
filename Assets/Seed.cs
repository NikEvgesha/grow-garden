using UnityEngine;

public class Seed : MonoBehaviour
{

    protected SeedItem _item;


    private void Awake()
    {
        _item = GetComponent<SeedItem>();
    }

}
