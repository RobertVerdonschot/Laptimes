using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroMvvm;
using LapTimes.Model;

namespace LapTimes.ViewModel
{
    class LapViewModel : ObservableObject
    {
        ILap _lap;

        public uint lapNumber { get { return _lap.lapNumber; } set { _lap.lapNumber = value; } }
        public string teamMember { get { return _lap.teamMember.name; } set { _lap.teamMember.name = value; } }
        public string startTime { get { return _lap.startTime.ToLongTimeString(); } private set { } }
        public string finishTime { get { return _lap.finishTime.ToLongTimeString(); } private set { } }
        public string lapTime { get { return TS2DT(_lap.lapTime).ToLongTimeString(); } private set { } }
        public string empty { get; private set; }

        public LapViewModel(ILap lap)
        {
            this._lap = lap;
            this._lap.LapChangedEvent += LapChangedEventHandler;
            this._lap.LapTimeChangedEvent += LapTimeChangedEventHandler;
        }

        public void LapChangedEventHandler(object sender, ILap e)
        {
            RaisePropertyChanged("lapNumber");
            RaisePropertyChanged("teamMember");
            RaisePropertyChanged("startTime");
            RaisePropertyChanged("finishTime");
            RaisePropertyChanged("lapTime");
        }

        public void LapTimeChangedEventHandler(object sender, ILap e)
        {
            RaisePropertyChanged("lapTime");
        }

        private DateTime TS2DT(TimeSpan ts)
        {
            return new DateTime(0) + ts;
        }
    }
}

