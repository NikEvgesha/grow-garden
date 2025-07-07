using UnityEngine;

public abstract class InteractablePoint : MonoBehaviour
{
    [SerializeField] protected TriggerEntryChecker _entryChecker;
    [SerializeField] protected GameObject _ui;


    protected void Start()
    {
        _entryChecker.PlayerInTrigger += SwitchUIVisibility;
    }

    protected void OnDestroy()
    {
        _entryChecker.PlayerInTrigger -= SwitchUIVisibility;
    }

    protected void SwitchUIVisibility(bool visible)
    {
        _ui.SetActive(visible);
    }
}