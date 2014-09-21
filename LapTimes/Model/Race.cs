using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    class Race : IRace, ILapObserver
    {
        string _raceName;
        IRaceSetup _raceSetup;
        IList<ILap> _laps = new List<ILap>();

        public string raceName { get { return _raceName; } set { _raceName = value; } }
        public IRaceSetup raceSetup { get { return _raceSetup; } set { _raceSetup = value; } }
        public IList<ILap> laps { get { return _laps; } set { _laps = value; } }

        public event EventHandler<ILap> LapsChangedEvent;
        public void OnLapChanged(ILap lap)
        {
            if (LapsChangedEvent != null)
            {
                LapsChangedEvent(this, lap);
            }
        }

    }
}
