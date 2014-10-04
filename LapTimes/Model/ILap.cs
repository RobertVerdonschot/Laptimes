using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    interface ILap
    {
        uint lapNumber { get; set; }
        ITeamMember teamMember { get; set; }
        DateTime startTime { get; set; }
        DateTime finishTime { get; set; }
        TimeSpan lapTime { get; set; }
        bool started { get; set; }
        bool finished { get; set; }

        event EventHandler<ILap> LapChangedEvent;
        event EventHandler<ILap> LapTimeChangedEvent;
    }
}
