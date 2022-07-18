using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorMethods
{
    internal class OperatorMethods
    {
        //산술 연산  
        //================================================================ 
        //더하기 
        static public int Sum(int a, int b)
        {
            return a + b;
        }
        
        static public float Sum(float a, float b)
        {
            return a + b;
        }
        //빼기 
        static public int Sub(int a, int b)
        {
            return a - b;
        }
        //나누기 
        static public int Div(int a, int b)
        {
            return a / b;
        }

        // 함수 오버로딩
        // 같은 기능을 수행하는 함수의 이름을 똑같이 하면서
        // 파라미터가 다르면 동일한 이름의 함수도 여러개 정의할 수 있는 기능
        static public float Div(float a, float b)
        {
            return a / b;
        }

        static public float Div(float a, float b, float c)
        {
            return a / b / c;
        }

        static public double Div(double a, double b)
        {
            return a / b;
        }

        //곱하기 
        static public int Mul(int a, int b)
        {
            return a * b;
        }
        //나머지 
        static public int Mod(int a, int b)
        {
            return a % b;
        }
        //증감 연산 
        //========================================================= 
        
        static public int Increase(int a) // 매개변수도 지역변수
        {
            return ++a;
        }
        static public int Decrease(int a)
        {
            return --a;
        }

        //관계 연산 
        //========================================================== 
        static public bool IsSame(int a, int b)
        {
            //return a == b;
            bool isSame = false; 
            if (a == b) 
                isSame = true; 
            return isSame;
        }
        static public bool IsDifferent(int a, int b)
        {
            return a != b;
            /*bool isDifferent = false; 
            if (a != b) 
                isDifferent = true; 
            return isDifferent;*/
        }
        static public bool IsBigger(int a, int b)
        {
            return a > b;
            /*bool isBigger = false; 
            if (a > b) 
                isBigger = true; 
            return isBigger;*/
        }
        static public bool IsSmaller(int a, int b)
        {
            return a < b;
            /*bool isSmaller = false; 
            if (a < b) 
                isSmaller = true; 
            return isSmaller;*/
        }
        static public bool IsBiggerOrSame(int a, int b)
        {
            return a >= b;
            /*bool isBiggerOrSame = false; 
            if (a >= b) 
                isBiggerOrSame = true; 
            return isBiggerOrSame;*/
        }
        static public bool IsSmallerOrSame(int a, int b)
        {
            return a <= b;
            /*bool isSmallerOrSame = false; 
            if (a <= b) 
                isSmallerOrSame = true; 
            return isSmallerOrSame;*/
        }

        // 대입연산 
        //========================================================= 
        static public int PlusBToA(int a, int b)
        {
            a += b;
            return a;
        }
        static public int MinusBToA(int a, int b)
        {
            a -= b;
            return a;
        }
        static public int MultiplyBToA(int a, int b)
        {
            a *= b;
            return a;
        }
        static public int DivideBToA(int a, int b)
        {
            a /= b;
            return a;
        }
        static public int ModBToA(int a, int b)
        {
            a %= b;
            return a;
        }

        // 논리 연산 
        // 양측의 피연산자들을 비교하여 연산 수행 
        //======================================================================= 
        static public bool LogicOR(bool a, bool b)
        {
            return a | b;
        }
        static public bool LogicAND(bool a, bool b)
        {
            return a & b;
        }
        static public bool LogicXOR(bool a, bool b)
        {
            return a ^ b;
        }
        static public bool LogicNOT(bool a)
        {
            return !a;
        }

        // 조건부 논리연산 
        // 왼쪽 피연산자 조건에 따라 오른쪽 피연산자와 비교할지 말지 평가 후 연산을 진행 
        //================================================================= 
        static public bool ConditionalLogicOR(bool a, bool b)
        {
            return a || b;
            // a 가 true 일 경우, b 의 값에 상관없이 결과가 true 이므로, b 를 평가하지않고 true 를 리턴. 
        }
        static public bool ConditionalLogicAND(bool a, bool b)
        {
            return a && b;
            // a 가 false 일 경우, b 의 값에 상관없이 결과가 false 이므로, b 를 평가하지않고 false 를 리턴. 
        }

        // 비트 연산 
        //======================================================== 
        static public int BitLogicOR(int a, int b)
        {
            return a | b;
        }
        static public int BitLogicAND(int a, int b)
        {
            return a & b;
        }
        static public int BitLogicXOR(int a, int b)
        {
            return a ^ b;
        }
        static public int BitShiftLeft(int a, int howManyBitYouWantToShift)
        {
            return a >> howManyBitYouWantToShift;
        }
        static public int BitShiftRight(int a, int howManyBitYouWantToShift)
        {
            return a >> howManyBitYouWantToShift;
        }
        static public int BitComplement(int a)
        {
            return ~a;
        }
    }
}
