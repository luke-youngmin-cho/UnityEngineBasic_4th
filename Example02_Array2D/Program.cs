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

        static Random randomX = new Random();
        static Random randomY = new Random();

        static void Main(string[] args)
        {
            CalcSpawnPoint(out int x, out int y);
            Player player = new Player(x, y);            
            player.MoveLeft(map);
        }

        // 재귀함수 : 함수가 자기 자신을 다시 호출하는 형태의 함수
        // out 키워드 : 함수가 반환될 때 두개 이상의 값을 반환하고자 할때 / 인자로 넘겨받은 
        // 변수에 연산결과를 반환해서 할당해야할때 파라미터의 자료형 앞에 쓴다.

        // out 키워드는 해당 함수가 반환될때 인자에 값을 할당하는 형태 (즉, 함수가 반환되어야 인자로 넣은 변수의 값이 변함)
        // 함수내에서 값을 할당하는 즉시 변하게 하고싶다면 ( 참조형식으로 쓰고싶다면 ) out 키워드 대신 ref 키워드를 사용하면 된다.
        static void CalcSpawnPoint(out int x, out int y)
        {
            y = randomY.Next(0, map.GetLength(0));
            x = randomX.Next(0, map.GetLength(1));
            
            if (map[y, x] != 0)
                CalcSpawnPoint(out x, out y);
        }
    }

    class Player
    {
        private int _x; // 현재 x 좌표
        private int _y; // 현재 y 좌표

        public Player(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// 플레이어를 현재 맵에서 왼쪽으로 이동시키는 함수
        /// </summary>
        /// <param name="map"> 이동시킬 기준이 되는 맵 </param>
        public void MoveLeft(int[,] map)
        {
            // 이동하려는 위치가 맵의 경계를 넘어가는지 체크
            if (_x - 1 < 0)
            {
                Console.WriteLine($"플레이어를 왼쪽으로 이동시킬 수 없습니다. (맵의 경계)");
            }
            // 이동하려는 위치가 이동가능하면 이동
            else if (map[_y, _x - 1] == 0)
            {
                _x--;
                Console.WriteLine($"플레이어 왼쪽으로 한칸 이동. 현재 위치 {_x}, {_y}");
            }
            // 이동하려는 위치가 벽이면 이동안함
            else if (map[_y, _x - 1] == 1)
            {
                Console.WriteLine($"플레이어를 왼쪽으로 이동시킬 수 없습니다. (벽)");
            }
        }

        public void MoveRight(int[,] map)
        {
            // 이동하려는 위치가 맵의 경계를 넘어가는지 체크
            if (_x + 1 > map.GetLength(1) - 1)
            {
                Console.WriteLine($"플레이어를 오른쪽으로 이동시킬 수 없습니다. (맵의 경계)");
            }
            // 이동하려는 위치가 이동가능하면 이동
            else if (map[_y, _x + 1] == 0)
            {
                _x++;
                Console.WriteLine($"플레이어 오른쪽으로 한칸 이동. 현재 위치 {_x}, {_y}");
            }
            // 이동하려는 위치가 벽이면 이동안함
            else if (map[_y, _x + 1] == 1)
            {
                Console.WriteLine($"플레이어를 오른쪽으로 이동시킬 수 없습니다. (벽)");
            }
        }

        public void MoveUp(int[,] map)
        {
            // 이동하려는 위치가 맵의 경계를 넘어가는지 체크
            if (_y - 1 < 0)
            {
                Console.WriteLine($"플레이어를 위쪽으로 이동시킬 수 없습니다. (맵의 경계)");
            }
            // 이동하려는 위치가 이동가능하면 이동
            else if (map[_y - 1, _x] == 0)
            {
                _y--;
                Console.WriteLine($"플레이어 위쪽으로 한칸 이동. 현재 위치 {_x}, {_y}");
            }
            // 이동하려는 위치가 벽이면 이동안함
            else if (map[_y - 1, _x] == 1)
            {
                Console.WriteLine($"플레이어를 위쪽으로 이동시킬 수 없습니다. (벽)");
            }
        }

        public void MoveDown(int[,] map)
        {
            // 이동하려는 위치가 맵의 경계를 넘어가는지 체크
            if (_y + 1 > map.GetLength(0) - 1)
            {
                Console.WriteLine($"플레이어를 아랫쪽으로 이동시킬 수 없습니다. (맵의 경계)");
            }
            // 이동하려는 위치가 이동가능하면 이동
            else if (map[_y + 1, _x] == 0)
            {
                _y++;
                Console.WriteLine($"플레이어 아랫쪽으로 한칸 이동. 현재 위치 {_x}, {_y}");
            }
            // 이동하려는 위치가 벽이면 이동안함
            else if (map[_y + 1, _x] == 1)
            {
                Console.WriteLine($"플레이어를 아랫쪽으로 이동시킬 수 없습니다. (벽)");
            }
        }
    }
}
