using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    class RaceSetup : IRaceSetup
    {
        uint _lapDistance;
        DateTime _raceDuration;
        DateTime _startTime;
        uint _lapsPerShift;
        IList<ITeamMember> _teamMembers;

        public uint lapDistance { get { return _lapDistance; } set { _lapDistance = value; } }
        public DateTime raceDuration { get { return _raceDuration; } set { _raceDuration = value; } }
        public DateTime startTime { get { return _startTime; } set { _startTime = value; } }
        public uint lapsPerShift { get { return _lapsPerShift; } set { _lapsPerShift = value; } }
        public IList<ITeamMember> teamMembers { get { return _teamMembers; } set { _teamMembers = value; } }
    }
}
