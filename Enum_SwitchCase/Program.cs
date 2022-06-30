using System;
// enum (enumerated type) 열거형 
// enum 의 기본요소는 모두 int 형
// enum 의 기본형은 한번에 하나의 요소만 표현가능하다.
enum PlayerState
{
    Idle,    // ... 00000000
    Attack,  // ... 00000001
    Jump,    // ... 00000010
    Walk,    // ... 00000011
    Run,     // ... 00000100
    Dash,    // ... 00000101
    Home,    // ... 00000110
}

// Flags Attribute
// ToString()에서 명시되지 않은 enum 요소에 대해서 다른 요소들로 구성되는 문자열로 변환해줄 수 있는 속성
[Flags]
enum PlayerStateFlags
{
    Idle = 0 << 0,    // ... 00000000
    Attack = 1 << 0,  // ... 00000001
    Jump = 1 << 1,    // ... 00000010
    Walk = 1 << 2,    // ... 00000100
    Run = 1 << 3,     // ... 00001000
    Dash = 1 << 4,    // ... 00010000
    Home = 1 << 5,    // ... 00100000
}

namespace Enum_SwitchCase
{
    internal class Program
    {
        static bool doAttack = true;
        static bool doJump;
        static bool doWalk;
        static bool doRun;
        static bool doDash;
        static bool doHome;

        static PlayerState initState = PlayerState.Attack;
        static PlayerStateFlags flags = PlayerStateFlags.Attack | PlayerStateFlags.Jump;

        

        static void Main(string[] args)
        {
            Console.WriteLine(flags);

            Warrior warrior = new Warrior();
            warrior.name = "지존전사";

            if (doAttack)
                warrior.Attack();
            else if (doJump)
                warrior.Jump();
            else if (doWalk)
                warrior.Walk();
            else if (doRun)
                warrior.Run();
            else if (doDash)
                warrior.Dash();
            else if (doHome)
                warrior.Home();
            else
                Console.WriteLine("전사 가만히 있음");

            
            switch (initState)
            {
                case PlayerState.Idle:
                    break;// 현재 구문을 빠져나오는 분기문 
                case PlayerState.Attack:
                    warrior.Attack();
                    break;
                case PlayerState.Jump:
                    warrior.Jump();
                    break;
                case PlayerState.Walk:
                    warrior.Walk();
                    break;
                case PlayerState.Run:
                    warrior.Run();
                    break;
                case PlayerState.Dash:
                    warrior.Dash();
                    break;
                case PlayerState.Home:
                    warrior.Home();
                    break;
                default:
                    break;
            }

            // 분기문
            // continue : 코드 흐름을 여기서 끝내고 실행중이 구문의 조건으로 돌아와서 계속 진행
            // break : 코드 흐름을 여기서 끝내고 실행중이 구문을 빠져나옴
            // return : 코드 흐름을 여기서 끝내고 함수 제어권 및 값 반환
            for (int i = 0; i < 5; i++)
            {
                int a = 5;
                continue;
                a = 3;
            }


            string studentName = "";
            switch (studentName)
            {
                case "철수":
                    break;
                case "영희":
                    break;
                default:
                    break;
            }

            // 동작 명령
            while (true)
            {
                Console.WriteLine("전사에게 명령을 내려주세요");
                string order = Console.ReadLine();
                if (order == "exit")
                    return;

                if (Enum.TryParse(order, out PlayerState orderState))
                {
                    switch (orderState)
                    {
                        case PlayerState.Idle:
                            break;
                        case PlayerState.Attack:
                            warrior.Attack();
                            break;
                        case PlayerState.Jump:
                            warrior.Jump();
                            break;
                        case PlayerState.Walk:
                            warrior.Walk();
                            break;
                        case PlayerState.Run:
                            warrior.Run();
                            break;
                        case PlayerState.Dash:
                            warrior.Dash();
                            break;
                        case PlayerState.Home:
                            warrior.Home();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("동작 입력이 올바르지 않습니다");
                }
            }
            



        }
    }

    class Warrior
    {
        public string name;
        public void Attack()
        {
            Console.WriteLine($"{name} (이)가 공격함");
        }

        public void Jump()
        {
            Console.WriteLine($"{name} (이)가 점프함");
        }

        public void Walk()
        {
            Console.WriteLine($"{name} (이)가 걸음");
        }

        public void Run()
        {
            Console.WriteLine($"{name} (이)가 달림");
        }

        public void Dash()
        {
            Console.WriteLine($"{name} (이)가 돌진함");
        }

        public void Home()
        {
            Console.WriteLine($"{name} (이)가 귀환함");
        }
    }
}
