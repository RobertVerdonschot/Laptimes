using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    class Lap : ILap
    {
        ILapObserver _parent;
        uint _lapNumber;
        ITeamMember _teamMember;
        DateTime _startTime;
        DateTime _finishTime;
        DateTime _lapTime;
        bool _started;
        bool _finished;

        public uint lapNumber { get { return _lapNumber; } set { _lapNumber = value; OnLapChanged(); } }
        public ITeamMember teamMember { get { return _teamMember; } set { _teamMember = value; OnLapChanged(); } }
        public DateTime startTime { get { return _startTime; } set { _startTime = value; OnLapChanged(); } }
        public DateTime finishTime { get { return _finishTime; } set { _finishTime = value; OnLapChanged(); } }
        public DateTime lapTime { get { return _lapTime; } set { _lapTime = value; OnLapChanged(); } }
        public bool started { get { return _started; } set { _started = value; OnLapChanged(); } }
        public bool finished { get { return _finished; } set { _finished = value; OnLapChanged(); } }

        public Lap( ILapObserver parent, uint lapNumber, ITeamMember teamMember, DateTime startTime, bool started, bool finished )
        {
            this._parent = parent;
            this._lapNumber = lapNumber;
            this._teamMember = teamMember;
            this._startTime = startTime;
            this._started = started;
            this._finished = finished;
        }

        public event EventHandler<ILap> LapChangedEvent;
        private void OnLapChanged()
        {
            _parent.OnLapChanged(this);

            if (LapChangedEvent != null)
            {
                LapChangedEvent(this, this);
            }
        }
    }
}
