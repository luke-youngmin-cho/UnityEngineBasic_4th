using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
// - 진행방식 -
//
//프로그램 시작시
//말 다섯마리를 만들고 ( 말 클래스 구현해야함 )
//각 다섯마리는 초당 10~20 (정수형) 범위 거리를 랜덤하게 움직임
//각각의 말이 거리 200 에 도달하면 말의 이름과 등수를 출력해줌
//
//말은
//이름, 달린거리 를 멤버변수로
//달리기 를 멤버 함수로 가짐.
//달리기 멤버함수는 입력받은 거리를 달린거리에 더해주어서 달린거리를 누적시키는 역할을 함.
//
//매초 달릴 때 마다 각 말들이 얼마나 거리를 이동했는지 콘솔창에 출력해줌.
//경주가 끝나면 1,2,3,4,5 등 말의 이름을 등수 순서대로 콘솔창에 출력해줌.
//
// - Hint -
//
//- System.Threading namespace 에 있는 Thread.Sleep(1000); 를 사용하면 현재 프로그램을 1초 지연시킬수 있음
//While 반복문에서 Thread.Sleep(1000); 을 추가하면 1초에 한번씩 반복문을 실행함.
//- 말들이 동시에 들어오는 경우에 대해서는 그냥 말 이름 순으로 등수 책정

namespace Example4_HorseRacing
{
    internal class Program
    {
        static Random random;
        static bool isGameFinished = false;
        static int minSpeed = 10;
        static int maxSpeed = 20;
        static int finishDistance = 200;
        static int grade = 1;
        static void Main(string[] args)
        {
            List<Horse> horses = new List<Horse>();
            List<Horse> finishedHorses = new List<Horse>();
            for (int i = 0; i < 5; i++)
                horses.Add(new Horse($"{i + 1} 번마"));


            int count = 0;
            // 경주 중
            while (isGameFinished == false)
            {
                Console.WriteLine($"========================= {count} 초 ========================");

                foreach (Horse horse in horses)
                {
                    if (horse.enabled)
                    {
                        random = new Random();
                        int tmpMoveDistance = random.Next(minSpeed, maxSpeed + 1);
                        horse.Run(tmpMoveDistance);
                        Console.WriteLine($"{horse.name} : {horse.distance}");
                        if (horse.distance >= finishDistance)
                        {
                            horse.grade = grade;
                            horse.enabled = false;
                            finishedHorses.Add(horse);
                            grade++;
                        }
                    }
                }

                if (grade > horses.Count)
                {
                    isGameFinished = true;
                    Console.WriteLine("경주 끝");
                    break;
                }

                // 각각의 말들을 달리게 함
                Thread.Sleep(1000);
                count++;
            }

            Console.WriteLine("========================= 결과 발표 =======================");           
            foreach (var sub in finishedHorses)
            {
                Console.WriteLine($"{sub.grade} 등 : {sub.name}");
            }

        }
    }

    public class Horse
    {
        public string name;
        public int distance;
        public bool enabled;
        public int grade;

        public Horse(string name)
        {
            this.name = name;
            grade = -1;
            enabled = true;
        }

        public void Run(int moveDistance)
        {
            distance += moveDistance;
        }
        
    }
}
