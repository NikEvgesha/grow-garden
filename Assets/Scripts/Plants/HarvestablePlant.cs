public class HarvestablePlant : Plant
{
    private float _weight;
    public float Weight => _weight;
    public void SetWeight(float weight)
    {
        _weight = weight;
    }
}