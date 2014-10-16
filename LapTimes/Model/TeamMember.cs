using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    class TeamMember : ITeamMember
    {
        string _name;
        TimeSpan _expectedLapTime;
        TimeSpan _averageLapTime;

        public string name { get { return _name; } set { _name = value; } }
        public TimeSpan expectedLapTime { get { return _expectedLapTime; } set { _expectedLapTime = value; } }
        public TimeSpan averageLapTime { get { return _averageLapTime; } set { _averageLapTime = value; } }

        public TeamMember(string name, TimeSpan expectedLapTime)
        {
            this.name = name;
            this._expectedLapTime = expectedLapTime;
            this._averageLapTime = expectedLapTime;
        }
    }
}
