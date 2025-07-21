using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _amount;
    [SerializeField] private Text _weight;
    [SerializeField] private GameObject _activeFrame;

    private InventoryItem _item;
    public InventoryItem Item => _item;

    private int index;


/*    public void Init(Item item)
    {
        if (item == null)
        {
            Clear();
            return;
        }
           


        _item = item;
        _icon.gameObject.SetActive(true);
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
    }*/

    public void Init(InventoryItem item)
    {
        if (item == null)
        {
            Clear();
            return;
        }



        _item = item;
        _icon.gameObject.SetActive(true);
        _icon.sprite = item.item.Data.Icon;
        if (item.item.Type == ItemType.Plant && item.item.TryGetComponent<HarvestablePlant>(out HarvestablePlant plant))
        {
            _weight.text = item.item.Weight.ToString("0.00");
            _amount.text = "";
        }
        else
        {
            _weight.text = "";
            _amount.text = item.amount.ToString();
        }
        _activeFrame.SetActive(_item.active);
    }


    public void SetAmount(int amount)
    {
        _amount.text = amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null)
            Inventory.Instance.TrySetActive(index);
    }



    private void Clear()
    {
        _amount.text = "";
        _weight.text = "";
        _icon.gameObject.SetActive(false);
        _activeFrame.SetActive(false);
        _item = null;
    }

    public void SetIndex(int idx)
    {
        index = idx;
    }
}