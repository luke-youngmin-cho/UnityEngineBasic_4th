using System;
/*
엔터키 입력으로 주사위를 굴립니다.
주사위를 굴리면 플레이어가 전진하고, 샛별칸에 도착하거나 지나갈 시 샛별에 대한 이벤트가 발생합니다.
총 칸은 1에서 20까지 있으며, 20을 넘어가면 다시 1부터 전진을 계속합니다.
5배수 칸은 샛별칸이고, 이 칸을 지나거나 도착하면 샛별을 획득할 수 있습니다.
5배수 칸에 도착할 때에는 샛별 획득 개수가 영구적으로 1 증가합니다.
샛별을 획득할 시 , 새로 얻은 샛별 수와 총 획득한 샛별 수를 보여줍니다.

콘솔 출력 :
주사위를 돌려서 어떤 칸에 도착하면,
해당 칸의 번호 (1~20 중 하나 ), 해당 칸이 어떤칸인지 (그냥 일반인지 샛별인지 ),
현재 샛별수는 몇개인지 , 남은 주사위 수는 몇개인지 콘솔창에 출력해주고
다시 주사위를 굴리라고 콘솔창에 출력해줌.
주사위를 다쓰면 모은 샛별 수를 출력해주고 게임을 종료함. (초기 주사위 갯수 20개)

### - Hint

만들어야 하는 클래스 :
TileMap(맵을 세팅하고 맵에대한 정보를 가지고 있을 클래스)
TileInfo(각 칸들의 정보를 멤버로 가지는 클래스)
TileInfo_Star(샛별칸을 위한 클래스.TileInfo 를 상속받고 샛별칸에 대한 특수 정보를 멤버로 추가함)
주사위는 아래처럼 콘솔창에 찍어서 보여주면 됨.
Console.WriteLine("┌───────────┐");
Console.WriteLine("│ ●      ●│");
Console.WriteLine("│           │");
Console.WriteLine("│     ●    │");
Console.WriteLine("│           │");
Console.WriteLine("│ ●      ●│");
Console.WriteLine("└───────────┘");
*/
namespace Example04_DiceGame
{
    internal class Program
    {
        static private int totalTile = 20; //맵타일수
        static private int currentStarPoint; // 현재 샛별수
        static private int totalDiceNumber = 20; // 총 주사위수
        static private int currentTileIndex = 0; // 현재 플레이어 위치 
        static private int previousTileIndex = 0; // 이전 플레이어 위치
        static private Random random;


        static void Main(string[] args)
        {
            // 맵 생성
            TileMap map = new TileMap();
            map.MapSetUp(totalTile);

            int currentDiceNumber = totalDiceNumber; // 현재 주사위 갯수

            while (currentDiceNumber > 0)
            {
                int diceValue = RollADice();
                currentDiceNumber--; // 주사위갯수 차감
                currentTileIndex += diceValue; // 주사위 눈금만큼 플레이어 전진

               
                // 플레이어가 샛별칸을 몇개 지났는지 체크 
                int passedStarTileNum = currentTileIndex / 5 - previousTileIndex / 5;
                for (int i = 0; i < passedStarTileNum; i++)
                {
                    int starTileIndex = (currentTileIndex / 5 - i) * 5;

                    if (starTileIndex > totalTile)
                        starTileIndex -= totalTile;

                    if (map.TryGetTileInfo(starTileIndex, out TileInfo tileInfo_star))
                    {
                        currentStarPoint += (tileInfo_star as TileInfo_Star).starValue;
                    }
                    else
                    {
                        throw new Exception("샛별 칸 정보를 가져오는데 실패 했습니다");
                    }
                }


                if (currentTileIndex > totalTile)
                    currentTileIndex -= totalTile;

                if (map.TryGetTileInfo(currentTileIndex, out TileInfo tileInfo))
                {
                    tileInfo.OnTile();
                }
                else
                {
                    throw new Exception("플레이어가 맵을 이탈했슴다");
                }

                previousTileIndex = currentTileIndex;
                Console.WriteLine($"현재 샛별점수 : {currentStarPoint}");
                Console.WriteLine($"남은 주사위 갯수 : {currentDiceNumber}");

            }

            Console.WriteLine($"게임 끝! 점수 : {currentStarPoint}");

        }

        static private int RollADice()
        {
            string userInput = "Default";
            while (userInput != "")
            {
                Console.WriteLine("엔터키로 주사위를 굴리세요..");
                userInput = Console.ReadLine();
            }
            random = new Random();
            int diceValue = random.Next(1, 7);
            Console.WriteLine($"주사위를 굴렸다..! 눈금은 {diceValue} 다!");
            DisplayDice(diceValue);
            return diceValue;
        }

        static private void DisplayDice(int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("└───────────┘");
                    break;
                case 2:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 3:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 4:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 5:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 6:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                default:
                    throw new Exception("주사위 눈금이 잘못되었어여");
            }
        }
    }
}
