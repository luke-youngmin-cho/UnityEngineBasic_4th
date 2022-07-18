using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example04_DiceGame
{
    internal class TileInfo_Star : TileInfo
    {
        public int starValue = 3; // 플레이어가 획득할 수있는  샛별 갯수 

        public override void OnTile()
        {
            base.OnTile();
            starValue++; // 플레이어가 획들할 수 있는 샛별 수 증가
        }
    }
}
