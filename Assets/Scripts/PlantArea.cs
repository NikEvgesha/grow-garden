using UnityEngine;
using UnityEngine.EventSystems;

public class PlantArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlantData _plant; // DELETE
    [SerializeField] private float _maxPlantDistance = 5f;

    public void OnPointerClick(PointerEventData eventData)
    {

        RaycastResult hit = eventData.pointerCurrentRaycast;

        float distance = (hit.worldPosition - GameManager.Instance.Player.transform.position).magnitude;

        if (distance < _maxPlantDistance)
        {
            // TODO: Get active seed in hand
            Plant plant = PlantsManager.Instance.GetPlantInstance(_plant);
            plant.transform.position = hit.worldPosition;
        } else
        {
            GameUI.Instance.Hints.ShowHint(UIHintType.TooFar);
        }

        


    }

}
