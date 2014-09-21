using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    interface IRaceSetup
    {
        uint lapDistance { get; set; }
        DateTime raceDuration { get; set; }
        DateTime startTime { get; set; }
        uint lapsPerShift { get; set; }
        IList<ITeamMember> teamMembers { get; set; }
    }
}
