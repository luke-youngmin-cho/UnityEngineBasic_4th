using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    private static ItemAssets _instance;
    public static ItemAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ItemAssets>("ItemAssets"));
            return _instance;
        }
    }


    [SerializeField] private List<Item> _itemList = new List<Item>();
    public Dictionary<int, Item> ItemDictionary = new Dictionary<int, Item>();

    private void Awake()
    {
        foreach (Item item in _itemList)
        {
            ItemDictionary.Add(item.Data.Code, item);
        }
    }
}
