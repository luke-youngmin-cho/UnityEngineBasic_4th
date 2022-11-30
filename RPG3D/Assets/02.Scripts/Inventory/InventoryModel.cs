using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryModel : INotifyCollectionChanged<ItemPair>
{
    public InventoryPresenter Presenter;

    public Dictionary<int, int> Items;
    public List<ItemPair> ItemList
    {
        get
        {
            return Items
                    .Select(x => new ItemPair(x.Key, x.Value))
                    .ToList();
        }
    }
        
    public event Action<ItemPair> ItemAdded;
    public event Action<ItemPair> ItemRemoved;
    public event Action CollectionChanged;


    public InventoryModel()
    {
        Debug.Log($"[InventoryModel] : Creating...");
        SetItems(InventoryData.Data.Items);
        Presenter = new InventoryPresenter(this);
        Debug.Log($"[InventoryModel] : Created...");
    }


    public void AddItem(ItemPair itemPair)
    {
        if (Items.ContainsKey(itemPair.Code))
            Items[itemPair.Code] += itemPair.Num;
        else
            Items.Add(itemPair.Code, itemPair.Num);

        ItemAdded?.Invoke(itemPair);
        CollectionChanged?.Invoke();

        InventoryData.Save(ItemList);
    }

    public bool RemoveItem(ItemPair itemPair)
    {
        if (Items.ContainsKey(itemPair.Code))
        {
            if (Items[itemPair.Code] > itemPair.Num)
                Items[itemPair.Code] -= itemPair.Num;
            else if (Items[itemPair.Code] == itemPair.Num)
                Items.Remove(itemPair.Code);
            else
                throw new Exception($"[InventoryModel] : �������� {Items[itemPair.Code]} �����ϰ� ������ {itemPair.Num} ���� �����Ϸ��� �õ��߽��ϴ�.");

            ItemRemoved?.Invoke(itemPair);
            CollectionChanged?.Invoke();

            InventoryData.Save(ItemList);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetItems(List<ItemPair> items)
    {
        foreach (ItemPair itemPair in items)
        {
            if (Items.ContainsKey(itemPair.Code))
                Items[itemPair.Code] += itemPair.Num;
            else
                Items.Add(itemPair.Code, itemPair.Num);
        }
    }
}
