using System;
using System.Collections.Generic;
using System.Text;

namespace MsLab7
{
    public class Position
    {
        public int CurrentNumberOfMarkers;
        public int MinCountOfMarkers = 0;
        public int MaxCountOfMarkers = 0;
        public double AvarageCountOfMarkers = 0;
        public string Name { get; set; }

        public Position(string name, int markersCount)
        {
            this.Name = name;
            this.CurrentNumberOfMarkers = markersCount;
            if (MinCountOfMarkers < markersCount)
            {
                MinCountOfMarkers = markersCount;
            }
        }

        public void markersStatistic()
        {
            if (CurrentNumberOfMarkers < MinCountOfMarkers)
                MinCountOfMarkers = CurrentNumberOfMarkers;
            if (CurrentNumberOfMarkers > MaxCountOfMarkers)
                MaxCountOfMarkers = CurrentNumberOfMarkers;
            AvarageCountOfMarkers += CurrentNumberOfMarkers;
        }
    }
}
