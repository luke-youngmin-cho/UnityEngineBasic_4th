using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataAssets : MonoBehaviour
{
    private static ItemDataAssets _instance;
    public static ItemDataAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ItemDataAssets>("ItemDataAssets"));
            return _instance;
        }
    }   


    [SerializeField] private List<ItemData> _itemDataList = new List<ItemData>();
    public Dictionary<int, ItemData> ItemDataDictionary = new Dictionary<int, ItemData>();

    private void Awake()
    {
        foreach (ItemData itemData in _itemDataList)
        {
            ItemDataDictionary.Add(itemData.Code, itemData);
        }
    }
}
