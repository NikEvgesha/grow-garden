using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class HarvestablePlant : Plant, IPointerClickHandler
{
    //[SerializeField] private InteractionArea _interactionArea;
    [SerializeField] protected float _maxHarvestDistance;
    protected Transform _spawnPoint;
    public float Weight => _plantData.BaseWeight * _weightMultiplier;
    public Action<Transform> OnHarvest;



    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Grown) return;
        Debug.Log("Click on plant");

        RaycastResult hit = eventData.pointerCurrentRaycast;

        float distance = (hit.worldPosition - GameManager.Instance.Player.transform.position).magnitude;

        if (distance < _maxHarvestDistance)
        {
            TryHarvest();
        }
        else
        {
            GameUI.Instance.Hints.ShowHint(UIHintType.TooFar);
        }

    }

    protected virtual void TryHarvest()
    {
        // TODO: collect yo Inventory
        if (_spawnPoint != null)
            OnHarvest.Invoke(_spawnPoint);

        Destroy(gameObject);
    }

    public void SetSpawnPoint(Transform point)
    {
        _spawnPoint = point;
    }

}