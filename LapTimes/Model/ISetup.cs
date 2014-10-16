using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    interface ISetup
    {
        uint lapDistance { get; set; }
        TimeSpan raceDuration { get; set; }
        DateTime startTime { get; set; }
        uint lapsPerShift { get; set; }
        IList<ITeamMember> team { get; set; }

        event EventHandler SetupChangedEvent;        
    }
}
