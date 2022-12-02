using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryViewSlot : MonoBehaviour, IPointerDownHandler
{
    public int ID;
    public bool IsEmpty => _itemCode == 0;
    private int _itemCode;
    public int ItemCode
    {
        get
        {
            return _itemCode;
        }
        set
        {
            _itemCode = value;
            if (value != 0)
                _icon.sprite = ItemDataAssets.Instance.ItemDataDictionary[value].Icon;
            else 
                _icon.sprite = null;
        }
    }
    [SerializeField] private Image _icon;

    private int _num;
    public int Num
    {
        get
        {
            return _num;
        }
        set
        {
            _num = value;
            if (value > 1)
                _numText.text = value.ToString();
            else if (value == 1)
                _numText.text = string.Empty;
            else
            {
                ItemCode = 0;
            }
        }
    }
    [SerializeField] private TMP_Text _numText;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsEmpty)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        { 
            if (InventorySlotHandler.instance.isBusy == false)
                InventorySlotHandler.instance.Handle(this);
        }
    }
}
