using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _amount;
    [SerializeField] private Text _weight;

    private Item _item;
    public Item Item => _item;


    public void Init(Item item)
    {
        _item = item;

        _icon.sprite = item.Data.Icon;
        if (item.Type == ItemType.Plant && item.TryGetComponent<HarvestablePlant>(out HarvestablePlant plant))
        {
            _weight.text = plant.Weight.ToString("0.00");
            _amount.text = "";
        } else
        {
            _weight.text = "";
            _amount.text = "1";
        }
        
    }

    public void SetAmount(int amount)
    {
        _amount.text = amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Inventory.Instance.TrySetActive(_item);
    }
}