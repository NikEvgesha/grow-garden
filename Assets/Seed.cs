using UnityEngine;

public class Seed : MonoBehaviour
{

    protected SeedItem _item;
    private SeedData _seedData;

    public SeedData Data => _seedData;

    private void Awake()
    {
        _item = GetComponent<SeedItem>();
    }

    public void Init(SeedData data)
    {
        _seedData = data;
    }

    public bool TryBuy()
    {
        return _item.TryBuy();
    }

}
