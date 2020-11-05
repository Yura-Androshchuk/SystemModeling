using System;
using System.Collections.Generic;

namespace MsLab7
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 3; i++)
            {
                Position p1 = new Position("[p1 - enter]", 1);
                Position p2 = new Position("[p2 - go to]", i);
                Position p3 = new Position("[p3 - go to]", 0);
                Position p4 = new Position("[p4 - go to]", i);
                Position p5 = new Position("[p5 - exit1]", 0);
                Position p6 = new Position("[p6 - exit2]", 0);
                Position p7 = new Position("[p7 - exit2]", 0);

                Transition t1 = new Transition("[income]");
                Transition t2 = new Transition("[procede 2-3]");
                Transition t3 = new Transition("[procede 3-4]");
                Transition t4 = new Transition("[exit1]");
                Transition t5 = new Transition("[exit2]");
                Transition t6 = new Transition("[exit3]");

                Arc a1 = new Arc("[from p1 to t1]", p1, t1, 1);
                Arc a2 = new Arc("[to p1]", p1, 1);
                Arc a3 = new Arc("[from t1 to p2]", p2, 1);
                Arc a4 = new Arc("[form p2 to t2]", p2, t2, 1);
                Arc a5 = new Arc("[from p2 to t6]", p2, t6, 1);
                Arc a6 = new Arc("[from t2 to p3]", p3, 1);
                Arc a7 = new Arc("[from t6 to p7]", p7, 1);
                Arc a8 = new Arc("[from p3 to t3]", p3, t3, 1);
                Arc a9 = new Arc("[from p3 t0 t5]", p3, t5, 1);
                Arc a10 = new Arc("[from t3 tp p4]", p4, 1);
                Arc a11 = new Arc("[from t3 tp p6]", p6, 1);
                Arc a12 = new Arc("[from p4 to t4]", p4, t4, 1);
                Arc a13 = new Arc("[from t4 to p5]", p5, 1);


                t1.InCommingArcs.Add(a1);
                t1.OutCommingArcs.Add(a2);
                t1.OutCommingArcs.Add(a3);
                t2.OutCommingArcs.Add(a6);
                t2.InCommingArcs.Add(a4);
                t6.OutCommingArcs.Add(a7);
                t6.InCommingArcs.Add(a5);
                t3.OutCommingArcs.Add(a10);
                t3.InCommingArcs.Add(a8);
                t4.OutCommingArcs.Add(a13);
                t4.InCommingArcs.Add(a12);
                t5.OutCommingArcs.Add(a11);
                t5.InCommingArcs.Add(a9);

                List<Position> positions = new List<Position>() { p1, p2, p3, p4, p5, p6, p7 };
                foreach (var p in positions)
                {
                    Console.WriteLine($"Markers number in {p.Name} = {p.CurrentNumberOfMarkers}");
                }
                List<Transition> transactions = new List<Transition>() { t1, t2, t3, t4, t5, t6 };
                Model model = new Model(positions, transactions);
                if (i == 0)
                    model.Simulate(150, true);
                else
                    model.Simulate(150, false); Console.WriteLine();

            }
            Console.ReadLine();
        }
    }
}
