using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public static InventoryView Instance;
    public InventoryPresenter Presenter;
    public List<InventoryViewSlot> Slots = new List<InventoryViewSlot>();
    [SerializeField] private InventoryViewSlot _slotPrefab;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        Debug.Log($"[InventoryView] : Initializing...");

        yield return new WaitUntil(() => Presenter != null);

        Presenter.source.OnSourceItemAdded += OnSourceItemAdded;
        Presenter.source.OnSourceItemRemoved += OnSourceItemRemoved;

        InventoryViewSlot slot;
        for (int i = 0; i < 36; i++)
        {
            slot = Instantiate(_slotPrefab, transform);
            slot.ID = i;
            Slots.Add(slot);
        }
        Debug.Log($"[InventoryView] : Initialized!!");
    }

    private void OnSourceItemAdded(ItemPair changed)
    {
        InventoryViewSlot slot = Slots.Find(slot => slot.ItemCode == changed.Code);
        if (slot)
        {
            slot.Num += changed.Num;
        }
        else
        {
            slot = Slots.Find(slot => slot.IsEmpty);
            if (slot)
            {
                slot.ItemCode = changed.Code;
                slot.Num = changed.Num;
            }
        }
    }

    private void OnSourceItemRemoved(ItemPair changed)
    {
        InventoryViewSlot slot = Slots.Find(slot => slot.ItemCode == changed.Code);
        if (slot)
        {
            slot.Num -= changed.Num;
        }
    }
}
