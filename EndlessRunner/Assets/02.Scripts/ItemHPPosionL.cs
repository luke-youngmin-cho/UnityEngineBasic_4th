using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHPPosionL : Item
{
    public override void OnEarn()
    {
        Player.instance.character.hp += 3;
    }
}
