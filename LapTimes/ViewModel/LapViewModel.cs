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
        public string startTime { get { return _lap.startTime.ToShortTimeString(); } private set { } }
        public bool started { get { return _lap.started; } set { _lap.started = value; } }
        public bool finished { get { return _lap.finished; } set { _lap.finished = value; } }

        public LapViewModel(ILap lap)
        {
            this._lap = lap;
            this._lap.LapChangedEvent += LapChangedEventHandler;
        }

        public void LapChangedEventHandler(object sender, ILap e)
        {
            RaisePropertyChanged("lapNumber");
            RaisePropertyChanged("teamMember");
            RaisePropertyChanged("startTime");
            RaisePropertyChanged("started");
            RaisePropertyChanged("finished");
        }
    }
}

