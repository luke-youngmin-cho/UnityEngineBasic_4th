using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        // Inventory setup
        InventoryModel inventoryModel = new InventoryModel();
        yield return new WaitUntil(() => InventoryData.Data != null);
        yield return new WaitUntil(() => InventoryView.Instance != null);
        InventoryView.Instance.Presenter = inventoryModel.Presenter;

        Debug.Log("[GameManager] : Initialized!!");
    }
}
