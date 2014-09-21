using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    interface ITeamMember
    {
        string name { get; set; }
        DateTime expectedLapTime  { get; set; }
    }
}
