using UnityEngine;

[CreateAssetMenu(fileName = "FishInfo_", menuName = "Fishing/CreateFishInfo")]
public class FishInfo : ScriptableObject
{
    [SerializeField] private GameObject _fish;
    [SerializeField] private float _strong = 1;
    [SerializeField] private float _difficulty = 0;
    [SerializeField] private float _chance;
    [SerializeField] private float _weightMin;
    [SerializeField] private float _weightMax;
    [SerializeField] private float _priceMultiply;

    public float GetDifficulty()
    {
        return _difficulty;
    }
    public float GetStrong()
    {
        return _strong;
    }

}
