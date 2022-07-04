using System;
using System.Collections.Generic;

namespace ClassInheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creature creature = new Creature();
            //creature.Breath();            

            Human human = new Human();
            human.name = "실험체1";
            human.Breath();
            human.Grow();
            human.Grow();

            YellowMan yellowMan = new YellowMan();
            yellowMan.name = "황인종1";
            yellowMan.Grow();
            BlackMan blackMan = new BlackMan();
            blackMan.name = "흑인종1";
            blackMan.Grow();
            WhiteMan whiteMan = new WhiteMan();
            whiteMan.name = "백인종1";
            whiteMan.Grow();

            // 자식객체는 부모타입으로 참조가 가능하다 (공변성)
            List<Human> men = new List<Human>();
            men.Add(new YellowMan());
            men.Add(new BlackMan());
            men.Add(new WhiteMan());
            for (int i = 0; i < men.Count; i++)
            {
                men[i].name = $"사람{i + 1}";
                men[i].Grow();
            }

            List<ITwoLeggedWalker> twoLeggedWalkers = new List<ITwoLeggedWalker>();
            twoLeggedWalkers.Add(new YellowMan());
            twoLeggedWalkers.Add(new BlackMan());
            twoLeggedWalkers.Add(new WhiteMan());
            for (int i = 0; i < men.Count; i++)
            {
                twoLeggedWalkers[i].TwoLeggedWalk();
            }
        }
    }
}
