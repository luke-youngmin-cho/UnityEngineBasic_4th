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
            {
                _data = Load();
            }
            return _data;
        }
        set
        {
            _data = value;
        }
    }
    public List<ItemPair> Items = new List<ItemPair>();

    public InventoryData()
    {
        Debug.Log($"[InventoryData] : Creating...");
        
        Debug.Log($"[InventoryData] : Created!!");
    }

    public static InventoryData Save(List<ItemPair> list)
    {
        Data.Items = list;

        if (Directory.Exists($"{Application.persistentDataPath}/InventoryDatas") == false)
            Directory.CreateDirectory($"{Application.persistentDataPath}/InventoryDatas");

        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/InventoryData.json";
        string jsonData = JsonUtility.ToJson(Data);
        File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"[InventoryData] : Data saved!!");
        return Data;
    }

    public static InventoryData CreateDefault()
    {
        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/InventoryData.json";

        if (Directory.Exists($"{Application.persistentDataPath}/InventoryDatas") == false)
            Directory.CreateDirectory($"{Application.persistentDataPath}/InventoryDatas");

        if (File.Exists(jsonPath) == false)
        {
            _data = new InventoryData();
            _data.Items = new List<ItemPair>();
            string jsonData = JsonUtility.ToJson(_data);
            File.WriteAllText(jsonPath, jsonData);
            Debug.Log($"[InventoryData] : Data created!!");
            return _data;
        }

        return null;
    }



    public static InventoryData Load()
    {
        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/InventoryData.json";

        try
        {
            if (Directory.Exists($"{Application.persistentDataPath}/InventoryDatas") == false ||
                File.Exists(jsonPath) == false)
            {
                return CreateDefault();
            }
            else
            {
                Debug.Log($"[InventoryData] : Data loaded!!");
                return JsonUtility.FromJson<InventoryData>(File.ReadAllText(jsonPath));
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }
}
