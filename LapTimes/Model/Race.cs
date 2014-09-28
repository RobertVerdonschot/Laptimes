using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LapTimes.Builder;

namespace LapTimes.Model
{
    class Race : IRace
    {
        public string raceName { get { return _raceName; } set { _raceName = value; } }
        public IRaceSetup raceSetup { get { return _raceSetup; } set { _raceSetup = value; } }
        public IList<ILap> laps { get { return _laps; } set { _laps = value; } }

        public Race()
        {
            _laps = new List<ILap>();
            _raceSetup = IOC.Get<IRaceSetup>();
        }

        public event EventHandler<ILap> LapsChangedEvent;
        public void OnLapChanged(ILap lap)
        {
            if (LapsChangedEvent != null)
            {
                LapsChangedEvent(this, lap);
            }
        }

        private string _raceName;
        private IRaceSetup _raceSetup;
        private IList<ILap> _laps;
    }
}
