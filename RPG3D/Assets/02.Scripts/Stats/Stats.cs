using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public interface IStats
{
    public Stats stats { get;}
}

public class Stats : MonoBehaviour
{
    public List<StatData> StatDataList = new List<StatData>();
    public Dictionary<int, Stat> StatDictionary;
    public Stat this[int id] => StatDictionary[id];

    private void Awake()
    {
        StatDictionary = new Dictionary<int, Stat>();
        foreach (var statData in StatDataList)
        {
            if (StatDictionary.ContainsKey(statData.ID) == false)
                StatDictionary.Add(statData.ID, new Stat(statData.ID, statData.Value, statData.Min, statData.Max));
        }
        Debug.Log("Stat initialized");
    }
}

public class Stat
{
    public const int ID_HP = 1;
    public const int ID_MP = 2;
    public const int ID_ATK = 3;
    public const int ID_DEF = 4;

    public int ID;
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (_value < value)
                OnValueIncreased?.Invoke(value);
            else if (_value > value)
                OnValueDecreased?.Invoke(value);

            if (value >= Max)
            {
                value = Max;
                OnValueMax?.Invoke();
            }
            else if (value <= Min)
            {
                value = Min;
                OnValueMin?.Invoke();
            }

            if (value != _value)
                OnValueChanged?.Invoke(value);

            _value = value;
            ModifiedValue = CalcModifiedValue();
        }
    }
    public int Min;
    public int Max;
    private int _modifiedValue;
    public int ModifiedValue
    {
        get
        {
            return _modifiedValue;
        }
        set
        {
            if (_modifiedValue != value)
            {
                _modifiedValue = value;
                OnModifiedValueChanged?.Invoke(value);
            }
        }
    }
    public List<StatModifier> ModifierList;
    public Action OnValueMax;
    public Action OnValueMin;
    public Action<int> OnValueChanged;
    public Action<int> OnValueDecreased;
    public Action<int> OnValueIncreased;
    public Action<int> OnModifiedValueChanged;

    public Stat(int id, int value, int min, int max)
    {
        ID = id;
        Value = value;
        Min = min;
        Max = max;
    }

    public void AddModifier(StatModifier modifier)
    {
        ModifierList.Add(modifier);
        ModifierList.Sort();
        ModifiedValue = CalcModifiedValue();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        ModifierList.Remove(modifier);
        ModifiedValue = CalcModifiedValue();
    }

    public int CalcModifiedValue()
    {
        int tempValue = Value;
        int sumPercentAdd = 0;

        for (int i = 0; i < ModifierList.Count; i++)
        {
            if (ModifierList[i].StatModType == StatModType.Flat)
            {
                tempValue += ModifierList[i].Value;
            }
            else if (ModifierList[i].StatModType == StatModType.PercentAdd)
            {
                sumPercentAdd += ModifierList[i].Value;
                
                if (i + 1 >= ModifierList.Count || ModifierList[i + 1].StatModType != StatModType.PercentAdd)
                {
                    tempValue *= 100 + sumPercentAdd;
                    tempValue /= 100;
                }
            }
            else if (ModifierList[i].StatModType == StatModType.PercentMul)
            {
                tempValue *= 100 + ModifierList[i].Value;
                tempValue /= 100;
            }
        }

        return tempValue;
    }
    
    public void Modify(int amount, StatModType statModType)
    {
        if (statModType == StatModType.Flat)
        {
            ModifiedValue += amount;
        }
        else if (statModType == StatModType.PercentAdd || 
                 statModType == StatModType.PercentMul)
        {
            ModifiedValue *= 100 + amount;
            ModifiedValue /= 100;
        }
    }
}

