using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Equipment,
    Spend,
    ETC
}

[CreateAssetMenu(fileName = "New ItemData", menuName = "RPG/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemTypes ItemType;
    public string Name;
    public string Description;
    public int MaxNum;
    public Sprite Icon;
    public int Code;
}
