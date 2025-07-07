using System;
using UnityEngine;

public class TriggerEntryChecker : MonoBehaviour
{
    private bool _playerInTrigger;

    public Action<bool> PlayerInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_playerInTrigger)
        {
            _playerInTrigger = !_playerInTrigger;
            PlayerInTrigger?.Invoke(_playerInTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _playerInTrigger)
        {
            _playerInTrigger = !_playerInTrigger;
            PlayerInTrigger?.Invoke(_playerInTrigger);
        }
    }
}
