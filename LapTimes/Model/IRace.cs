using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    interface IRace
    {
        string raceName { get; set; }
        IRaceSetup raceSetup { get; set; }
        IList<ILap> laps  { get; set; }

        event EventHandler<ILap> LapsChangedEvent;
    }
}
