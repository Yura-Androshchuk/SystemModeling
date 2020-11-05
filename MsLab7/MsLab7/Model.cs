using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsLab7
{
    public class Model
    {
        public List<Position> positions = new List<Position>();
        public List<Transition> transitions = new List<Transition>();
        public List<Transition> nextTransitions = new List<Transition>();
        Random random = new Random();

        public Model(List<Position> positions, List<Transition> transitions)
        {
            this.positions = positions;
            this.transitions = transitions;
        }

        public void Simulate(int numberOfIterations, bool ver)
        {
            int iterator = 0;
            while (iterator < numberOfIterations)
            {
                if (ver)
                {
                    Console.WriteLine($"Number of iteration {iterator + 1}");
                }
                foreach (var p in positions)
                {
                    p.markersStatistic();
                }
                foreach (var t in transitions)
                {
                    if (t.TransitionChance(positions))
                    {
                        nextTransitions.Add(t);
                    }
                }
                foreach (var t in nextTransitions)
                {
                    t.choiceProbability = (Double)1 / nextTransitions.Count();
                }
                double r = random.NextDouble();
                for (int i = 0; i < nextTransitions.Count; i++)
                {
                    if (r < nextTransitions[i].choiceProbability)
                    {
                        positions = nextTransitions[i].MakeTransition(positions, ver);
                        break;
                    }
                    else
                        r -= nextTransitions[i].choiceProbability;
                }
                nextTransitions.Clear();
                iterator++;
            }
            if (ver)
                Console.WriteLine("Verification");
            Verification(numberOfIterations);
        }

        public void Verification(int iterations)
        {
            Console.WriteLine();
            Console.WriteLine("Statistics");
            Console.WriteLine("{0,16} {1,4} {2,4} {3,22}", "Name", "min", "max", "average");
            foreach (var p in positions)
            {
                p.AvarageCountOfMarkers /= iterations;
                Console.WriteLine("{0,16} {1,4} {2,4} {3,22}", p.Name, p.MinCountOfMarkers, p.MaxCountOfMarkers, p.AvarageCountOfMarkers);
            }
        }
    }
}
