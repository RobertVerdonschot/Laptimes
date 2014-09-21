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
        DateTime _expectedLapTime;

        public string name { get { return _name; } set { _name = value; } }
        public DateTime expectedLapTime { get { return _expectedLapTime; } set { _expectedLapTime = value; } }

        public TeamMember (string name)
        {
            this.name = name;
        }
    }
}
