using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    public Stats stats { get;}
}

public class Stats : MonoBehaviour
{
    public Dictionary<int, Stat> StatDictionary;
}

public class Stat
{
    public const int ID_HP = 1;
    public const int ID_MP = 2;
    public const int ID_ATK = 3;
    public const int ID_DEF = 4;

    public int ID;
    public int Value;
    public int Min;
    public int Max;
    public int ModifiedValue;
    public List<StatModifier> ModifierList;
    public Action OnValueMax;
    public Action OnValueMin;
    public Action<int> OnValueChanged;
    public Action<int> OnModifiedValueChanged;

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
