using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    class Lap : ILap
    {
        uint _lapNumber;
        ITeamMember _teamMember;
        DateTime _startTime;
        DateTime _finishTime;
        DateTime _lapTime;
        bool _started;
        bool _finished;

        public uint lapNumber { get { return _lapNumber; } set { _lapNumber = value; } }
        public ITeamMember teamMember { get { return _teamMember; } set { _teamMember = value; } }
        public DateTime startTime { get { return _startTime; } set { _startTime = value; } }
        public DateTime finishTime { get { return _finishTime; } set { _finishTime = value; } }
        public DateTime lapTime { get { return _lapTime; } set { _lapTime = value; } }
        public bool started { get { return _started; } set { _started = value; } }
        public bool finished { get { return _finished; } set { _finished = value; } }

        public Lap( uint lapNumber, ITeamMember teamMember, DateTime startTime, bool started, bool finished )
        {
            this._lapNumber = lapNumber;
            this._teamMember = teamMember;
            this._startTime = startTime;
            this._started = started;
            this._finished = finished;
        }
    }
}
