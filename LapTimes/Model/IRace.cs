using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    public delegate void RaceEventHandler();

    interface IRace : ILapObserver
    {
        string raceName { get; set; }
        ISetup setup { get; set; }
        IList<ILap> laps  { get; set; }
        
        void OnRaceChanged();
        event RaceEventHandler RaceChangedEvent;
    }
}
