using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    private LinkedList<HarvestablePlant> _plantsInTrigger;
    public float detectionRadius = 2f; // Радиус обнаружения
    public LayerMask targetLayer; // Слой объектов, которые нужно обнаруживать
    private bool _haveActiveCanvas = false;
    private Collider _activePlantCollider;
    private HarvestablePlant _activeHarvestable;


    private void Awake()
    {
        _plantsInTrigger = new LinkedList<HarvestablePlant>();
    }

    void Update()
    {

        CheckNearPlants();

        if (_haveActiveCanvas && PlayerInput.Instance.Interaction)
        {
            Harvest();
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.TryGetComponent<HarvestablePlant>(out HarvestablePlant harvestable) && harvestable.Grown) {
            _plantsInTrigger.AddLast(harvestable);
            if (!_haveActiveCanvas)
            {
                harvestable.ShowHarvestHint(true);
                _haveActiveCanvas = true;
            }
        }
    }*/


    private void CheckNearPlants()
    {
        // Проверяем, есть ли объекты в радиусе
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        if (_activePlantCollider != null && colliders.Contains(_activePlantCollider)) return;

        if (_activeHarvestable != null)
        {
            _activeHarvestable.ShowHarvestHint(false);
        }

        _haveActiveCanvas = false;
        _activePlantCollider = null;
        _activeHarvestable = null;

        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.transform.parent.parent.TryGetComponent<HarvestablePlant>(out HarvestablePlant harvestable))
                {
                    harvestable.ShowHarvestHint(true);            
                    _haveActiveCanvas = true;
                    _activePlantCollider = collider;
                    _activeHarvestable = harvestable;
                    break;
                }
            }

        }
    }

/*
    private void OnTriggerExit(Collider other)
    {
        if (_plantsInTrigger.Count == 0) return;

        if (other.transform.TryGetComponent<HarvestablePlant>(out HarvestablePlant harvestable) && harvestable.Grown)
        {
            _plantsInTrigger.Remove(harvestable);
            harvestable.ShowHarvestHint(false);
        }
    }*/


    private void Harvest()
    {
        if (_activeHarvestable == null) return;

        _activeHarvestable.ShowHarvestHint(false);
        _activePlantCollider = null;
        _haveActiveCanvas = false;
        _activeHarvestable.TryHarvest();
        _activeHarvestable = null;
        
    }
}
