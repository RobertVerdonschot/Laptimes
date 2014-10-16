using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.Model
{
    class Setup : ISetup
    {
        uint _lapDistance;
        TimeSpan _raceDuration;
        DateTime _startTime;
        uint _lapsPerShift;
        IList<ITeamMember> _team;

        public uint lapDistance 
        { 
            get { return _lapDistance; } 
            set { _lapDistance = value; } 
        }
        public TimeSpan raceDuration 
        { 
            get { return _raceDuration; } 
            set { _raceDuration = value; } 
        }
        public DateTime startTime 
        { 
            get { return _startTime; } 
            set { _startTime = value; } 
        }
        public uint lapsPerShift 
        { 
            get { return _lapsPerShift; } 
            set { _lapsPerShift = value; } 
        }
        public IList<ITeamMember> team 
        { 
            get { return _team; } 
            set { _team = value; } 
        }

        public event EventHandler SetupChangedEvent;
        private void OnSetupChanged()
        {
            if (SetupChangedEvent != null)
            {
                SetupChangedEvent(this, new EventArgs());
            }
        }

    }
}
