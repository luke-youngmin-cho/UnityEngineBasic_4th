using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

[System.Serializable]
public struct ItemPair
{
    public int Code;
    public int Num;

    public ItemPair(int code, int num)
    {
        Code = code;
        Num = num;
    }
}

public class InventoryData
{
    private static InventoryData _data;
    public static InventoryData Data
    {
        get
        {
            if (_data == null)
                _data = new InventoryData();
            return _data;
        }
        set
        {
            _data = value;
        }
    }
    public List<ItemPair> Items = new List<ItemPair>();

    public static void Save(List<ItemPair> list)
    {
        Data.Items = list;

        if (Directory.Exists($"{Application.persistentDataPath}/InventoryDatas") == false)
            Directory.CreateDirectory($"{Application.persistentDataPath}/InventoryDatas");

        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/InventoryData.json";
        string jsonData = JsonUtility.ToJson(Data);
        File.WriteAllText(jsonPath, jsonData);
    }

    public static void Load()
    {
        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/InventoryData.json";

        if (Directory.Exists($"{Application.persistentDataPath}/InventoryDatas") == false)
            Directory.CreateDirectory($"{Application.persistentDataPath}/InventoryDatas");
        else
        {
            if (File.Exists(jsonPath))
            {
                 Data = JsonUtility.FromJson<InventoryData>(File.ReadAllText(jsonPath));
            }
        }
    }
}
