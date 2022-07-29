using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfoGoldenDice : TileInfo
{
    public override void OnTile()
    {
        base.OnTile();
        GameManager.instance.goldenDiceNum++;
    }
}
