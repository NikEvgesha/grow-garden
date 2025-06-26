using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _amount;
    [SerializeField] private Text _weight;

    private InventoryItem _item;
    public InventoryItem Item => _item;


    public void Init(InventoryItem item)
    {
        _item = item;

        _icon.sprite = item.item.Icon;
        _amount.text = (item.amount == 0) ? "" : item.amount.ToString();
        _weight.text = (item.weight == 0) ? "" : item.weight.ToString("0.00");
    }
}