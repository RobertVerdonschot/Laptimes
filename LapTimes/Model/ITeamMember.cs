using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicroMvvm;

namespace LapTimes.Model
{
    interface ITeamMember 
    {
        string name { get; set; }
        TimeSpan expectedLapTime  { get; set; }
        TimeSpan averageLapTime { get; set; }
    }
}
