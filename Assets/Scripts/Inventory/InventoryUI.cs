using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private DynamicGridSpawner _grid;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private Text _mainCapacityText;



    private List<InventorySlot> _slotsUsable;
    private List<InventorySlot> _slotsMain;


    private void Awake()
    {
        _slotsUsable = new List<InventorySlot>();
        _slotsMain = new List<InventorySlot>();
    }
    private void Start()
    {
        Inventory.Instance.InventoryUsableUpdate += UpdateUI;
        Inventory.Instance.InventoryMainUpdate += UpdateMainUI;


        for (int i = 0; i < Inventory.Instance.CapacityUsable; i++)
        {
            InventorySlot slot = Instantiate(_slotPrefab, _slotsParent);
            slot.Init(null);
            slot.SetIndex(i);
            _slotsUsable.Add(slot);
        }

        for (int i = 0; i < Inventory.Instance.CapacityMain; i++)
        {
            InventorySlot slot = _grid.SpawnObject<InventorySlot>(_slotPrefab.gameObject);
            slot.Init(null);
            _slotsMain.Add(slot);
        }
    }

    private void OnDisable()
    {
        Inventory.Instance.InventoryUsableUpdate -= UpdateUI;
        Inventory.Instance.InventoryMainUpdate -= UpdateMainUI;
    }

    private void UpdateUI(List<InventoryItem> items)
    {


        for (int i = 0; i < _slotsUsable.Count; i++)
        {
            if (i >= items.Count)
            {
                _slotsUsable[i].Init(null);
            }
            else
            {
                _slotsUsable[i].Init(items[i]);
            }
        }
    }


    private void UpdateMainUI(List<InventoryItem> items)
    {
        int total = 0;
        for (int i = 0; i < _slotsMain.Count; i++)
        {
            if (items[i] != null)
                total++;
            _slotsMain[i].Init(items[i]);
        }
        _mainCapacityText.text = total.ToString() + "/" + Inventory.Instance.CapacityMain.ToString();
    }


    public void ToggleMainUI()
    {
        _mainUI.SetActive(!_mainUI.activeInHierarchy);
    }
}