using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    internal class YellowMan : Human
    {
        // override : virtual 함수를 재정의하는 키워드
        public override void Grow()
        {
            mass += 13.0f;
            height += 8.0f;
            Console.WriteLine($"{name} 이 자랐다 ! {mass}, {height}");
        }

        public void GoAcademy()
        {

        }
    }
}
