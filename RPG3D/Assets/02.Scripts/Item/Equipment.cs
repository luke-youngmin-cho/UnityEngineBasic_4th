using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    protected StatModifier MOD_HP, MOD_MP, MOD_ATK, MOD_DEF;

    public Equipment()
    {
        InitStatModifiers();
    }

    protected virtual void InitStatModifiers()
    {
        MOD_HP = new StatModifier(Stat.ID_HP, 10, StatModType.Flat);
        MOD_MP = new StatModifier(Stat.ID_MP, 20, StatModType.Flat);
        MOD_ATK = new StatModifier(Stat.ID_ATK, 10, StatModType.PercentAdd);
        MOD_DEF = new StatModifier(Stat.ID_DEF, 10, StatModType.PercentAdd);
    }

    public void Equip(CharacterBase character)
    {
        character.stats.StatDictionary[Stat.ID_HP].AddModifier(MOD_HP);
        character.stats.StatDictionary[Stat.ID_MP].AddModifier(MOD_MP);
        character.stats.StatDictionary[Stat.ID_ATK].AddModifier(MOD_ATK);
        character.stats.StatDictionary[Stat.ID_DEF].AddModifier(MOD_DEF);
    }

    public void Unequip(CharacterBase character)
    {
        character.stats.StatDictionary[Stat.ID_HP].RemoveModifier(MOD_HP);
        character.stats.StatDictionary[Stat.ID_MP].RemoveModifier(MOD_MP);
        character.stats.StatDictionary[Stat.ID_ATK].RemoveModifier(MOD_ATK);
        character.stats.StatDictionary[Stat.ID_DEF].RemoveModifier(MOD_DEF);
    }
}
