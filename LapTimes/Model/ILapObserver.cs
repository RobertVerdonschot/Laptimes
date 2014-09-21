using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    interface ILapObserver
    {
        void OnLapChanged(ILap lap);
    }
}
