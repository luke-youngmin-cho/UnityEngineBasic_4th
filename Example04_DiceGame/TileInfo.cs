using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example04_DiceGame
{
    internal class TileInfo
    {
        public int index; // 몇번째 칸인지에대한 정보
        public string name;
        public string discription;

        public virtual void OnTile()
        {
            Console.WriteLine($"{index} 번째 칸에 도착 : {name}, {discription}");
        }
    }
}
