using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InventoryPresenter
{
    private InventoryModel _model;

    #region Source
    public class Source
    {
        private InventoryModel _model;

        public Dictionary<int, int> Items;
        public int this[int key] { get => Items[key] ; set => Items[key] = value; }

        public event Action<ItemPair> OnSourceItemAdded;
        public event Action<ItemPair> OnSourceItemRemoved;

        public Source(InventoryModel model)
        {
            _model = model;
            Items = model.Items;

            _model.ItemAdded += (itemPair) =>
            {
                OnSourceItemAdded?.Invoke(itemPair);
            };

            _model.ItemRemoved += (itemPair) =>
            {
                OnSourceItemRemoved?.Invoke(itemPair);
            };
        }

        public void Add(int key, int value)
        {
            _model.AddItem(new ItemPair(key, value));
        }

        public bool Remove(int key, int value)
        {
            return _model.RemoveItem(new ItemPair(key, value));
        }

    }
    public Source source;
    #endregion

    public InventoryPresenter(InventoryModel model)
    {
        Debug.Log($"[InventoryPresenter] : Creating...");
        _model = model;
        source = new Source(model);

        Debug.Log($"[InventoryPresenter] : Created...");
    }
}
