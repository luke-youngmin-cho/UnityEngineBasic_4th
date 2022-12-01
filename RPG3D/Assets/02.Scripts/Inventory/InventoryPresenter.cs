using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

public class InventoryPresenter
{
    public static InventoryPresenter instance;
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

    #region Add Command
    public class AddCommand
    {
        private InventoryModel _model;

        public AddCommand(InventoryModel model)
        {
            _model = model;
        }

        public bool CanExecute(Item item)
        {
            return _model.Items.ContainsKey(item.Data.Code) || _model.Items.Count < 36;
        }

        public void Execute(Item item)
        {
            _model.AddItem(new ItemPair(item.Data.Code, item.Num));
        }

        public bool TryExecute(Item item)
        {
            if (CanExecute(item))
            {
                Execute(item);
                return true;
            }

            return false;
        }
    }
    public AddCommand addCMD;
    #endregion

    #region Remove Command
    public class RemoveCommand
    {
        private InventoryModel _model;

        public RemoveCommand(InventoryModel model)
        {
            _model = model;
        }

        public bool CanExecute(Item item)
        {
            return _model.Items.ContainsKey(item.Data.Code) &&
                   _model.Items[item.Data.Code] >= item.Num;
        }

        public void Execute(Item item)
        {
            _model.RemoveItem(new ItemPair(item.Data.Code, item.Num));
        }

        public bool TryExecute(Item item)
        {
            if (CanExecute(item))
            {
                Execute(item);
                return true;
            }

            return false;
        }
    }
    public RemoveCommand removeCMD;
    #endregion


    public InventoryPresenter(InventoryModel model)
    {
        Debug.Log($"[InventoryPresenter] : Creating...");
        instance = this;
        _model = model;
        source = new Source(model);
        addCMD = new AddCommand(model);
        removeCMD = new RemoveCommand(model);
        Debug.Log($"[InventoryPresenter] : Created...");
    }
}
