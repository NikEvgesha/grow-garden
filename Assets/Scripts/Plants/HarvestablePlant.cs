using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class HarvestablePlant : Plant, IPointerClickHandler
{
    //[SerializeField] private InteractionArea _interactionArea;
    [SerializeField] protected float _maxHarvestDistance;
    [SerializeField] protected GameObject _harvestCanvas;
    [SerializeField] protected GameObject _growCanvas;
    protected Transform _spawnPoint;
    protected HarvestPlantItem _item;
    public virtual float Weight => Mathf.Round(_weight * 100f) / 100f;
    public Action<Transform> OnHarvest;


    private void Awake()
    {
        _item = GetComponent<HarvestPlantItem>();
    }

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

    public virtual void TryHarvest()
    {
        // TODO: collect yo Inventory
        if (!_item.TryHarvest()) return;

        if (_spawnPoint != null)
            OnHarvest.Invoke(_spawnPoint);

        StopAllCoroutines();

    }

    public void SetSpawnPoint(Transform point)
    {
        _spawnPoint = point;
    }


    public void ShowHarvestHint(bool visible)
    {
        _harvestCanvas.SetActive(visible);
    }



}