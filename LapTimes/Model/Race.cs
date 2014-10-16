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
        public string raceName { get { return _raceName; } set { _raceName = value; OnRaceChanged(); } }
        public ISetup setup { get { return _raceSetup; } set { _raceSetup = value; OnRaceChanged(); } }
        public IList<ILap> laps { get { return _laps; } set { _laps = value; OnRaceChanged(); } }

        public Race()
        {
            _laps = new List<ILap>();
            _raceSetup = IOC.Get<ISetup>();
        }

        public event RaceEventHandler RaceChangedEvent;
        public void OnRaceChanged()
        {
            if (RaceChangedEvent != null)
            {
                RaceChangedEvent();
            }
        }

        public void OnLapChanged(ILap lap)
        {
            OnRaceChanged();
        }

        private string _raceName;
        private ISetup _raceSetup;
        private IList<ILap> _laps;
    }
}
