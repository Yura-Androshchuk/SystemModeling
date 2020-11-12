using System;
using System.Collections.Generic;
using System.Text;

namespace MsLab7
{
    public class Arc
    {
        public Transition nextTransition { get; set; } 
        public Position NextPos { get; set; }
        public Position PreviousPos { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

        public Arc(string name, Position next, int n) //constructor for arc where next element is a position
        {
            this.Name = name;
            PreviousPos = next;
            this.Number = n;
        }

        public Arc(string name, Position previoustP, Transition nextT, int n) //constructor for arc where next element is a transition
        {
            this.Name = name;
            nextTransition = nextT;
            NextPos = previoustP;
            this.Number = n;
        }
    }
}
