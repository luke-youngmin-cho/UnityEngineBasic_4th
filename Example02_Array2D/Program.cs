using System;

namespace Example02_Array2D
{
    internal class Program
    {
        // 0 은 갈수 있는 길 , 1은 벽
        static int[,] map = new int[5, 5]
        {
            { 0, 0, 0, 0, 1 },
            { 0, 1, 1, 1, 1 },
            { 0, 0, 0, 1 ,1 },
            { 1, 1, 0, 0, 0 },
            { 0, 1, 1, 0, 0 }
        };
        static void Main(string[] args)
        {
            Player player = new Player();
            player.x = 3;
            player.y = 0;
            player.MoveLeft(map);
        }
    }

    class Player
    {
        public int x;
        public int y;

        public void MoveLeft(int[,] map)
        {
            if (x - 1 < 0)
            {
                Console.WriteLine($"플레이어를 왼쪽으로 이동시킬 수 없습니다. (맵의 경계)");
            }
            else if (map[y, x - 1] == 0)
            {
                x--;
                Console.WriteLine($"플레이어 왼쪽으로 한칸 이동. 현재 위치 {x}, {y}");
            }
            else if (map[y, x - 1] == 1)
            {
                Console.WriteLine($"플레이어를 왼쪽으로 이동시킬 수 없습니다. (벽)");
            }
        }
    }
}
