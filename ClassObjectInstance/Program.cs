using System;

namespace ClassObjectInstance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 값형식 , 참조형식
            // 값형식 : 값을 직접 읽고 / 쓰는 형태의 형식
            // 참조형식 : 주소를 읽고 / 주소위치에 값을 쓰는 형태의 형식

            // int : 정수 타입
            // a : 정수 타입 변수
            int a = 0;
            // Human : 클래스 타입
            // human1 : 객체
            Human human1 = new Human(); // new 키워드 : 동적할당하는 키워드

        }
    }

    class Human
    {
        public static int age; 
        private float height; 
        double weight; 
        bool isResting;
        char genderChar;
        string name; 

        // 생성자
        public Human()
        {

        }
    }
}
