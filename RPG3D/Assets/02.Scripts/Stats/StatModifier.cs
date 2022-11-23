using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMul
}

public class StatModifier : IComparable<StatModifier>
{
    public readonly int ID;
    public readonly int Value;
    public readonly StatModType StatModType;

    public StatModifier(int id, int value, StatModType statModType)
    {
        ID = id;
        Value = value;
        StatModType = statModType;
    }

    public int CompareTo(StatModifier other)
    {
        if (this.StatModType < other.StatModType)
            return -1;
        else if (this.StatModType > other.StatModType)
            return 1;
        else
            return 0;
    }
}
