using System.Collections;
using UnityEngine;

/*public class GrowStage : MonoBehaviour
{
    [SerializeField] private GameObject stageModel;

    private float currentTime;
    private Plant _plant;
    private float duration;
    public void Activate(Plant plant)
    {
        _plant = plant;
        stageModel.SetActive(true);
        currentTime = 0;
        IEnumerator growCoroutine = Grow();
        StartCoroutine(growCoroutine);
    }



    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(duration);

        if (nextStage != null)
        {
            stageModel.SetActive(false);
            nextStage.Activate(_plant);
        } else
        {
            _plant.OnGrowFinish();
        }
    }
}*/