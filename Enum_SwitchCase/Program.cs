using System;
// enum (enumerated type) 열거형
// enum 의 기본요소는 모두 int 형
enum PlayerState
{
    Idle,
    Attack,
    Jump,
    Walk,
    Run,
    Dash,
    Home,
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

        static void Main(string[] args)
        {
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
