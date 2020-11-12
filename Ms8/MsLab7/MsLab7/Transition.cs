using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsLab7
{
    public class Transition
    {
        public List<Arc> InCommingArcs = new List<Arc>();
        public List<Arc> OutCommingArcs = new List<Arc>();
        public string name = "";
        public double choiceProbability = 0;

        public Transition(string name)
        {
            this.name = name;
        }

        public bool TransitionChance(List<Position> positions)
        {
            bool f = true;
            List<string> fromPositionsNames = InCommingArcs.Select(x => x.NextPos.Name).ToList();
            List<Position> connectedPositions = positions.Where(x => fromPositionsNames.Contains(x.Name) == true).ToList();
            for (int i = 0; i < connectedPositions.Count; i++)
            {
                if (connectedPositions[i].CurrentNumberOfMarkers < InCommingArcs[i].Number)
                {
                    f = false;
                    break;
                }
            }
            return f;
        }

        public List<Position> MakeTransition(List<Position> positions, bool istrue)
        {
            if (istrue)
            {
                Console.WriteLine($"Transition {name}:");
                Console.Write("Takes markers from: ");
            }
            foreach (var a in InCommingArcs)
            {
                positions.Where(x => x.Name == a.NextPos.Name).FirstOrDefault().CurrentNumberOfMarkers -= a.Number;
                if (istrue)
                    Console.Write($"=> {a.NextPos.Name} ");
            }
            if (istrue)
            {
                Console.WriteLine();
                Console.Write("Sends markers to: ");
            }
            foreach (var a in OutCommingArcs)
            {
                positions.Where(x => x.Name == a.PreviousPos.Name).FirstOrDefault().CurrentNumberOfMarkers += a.Number;
                if (istrue)
                {
                    Console.Write($"=> { a.PreviousPos.Name} ");
                }
            }
            if (istrue)
                Console.WriteLine();
            return positions;
        }

    }
}
