using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicroMvvm;
using LapTimes.Model;

namespace LapTimes.ViewModel
{
    class MemberViewModel : ObservableObject
    {
        public MemberViewModel(ITeamMember member, ViewModelHelper.ShowMessage ShowMessage )
        {
            this.member = member;
            this.ShowMessage = ShowMessage;
        }

        public string name 
        {
            get { return member.name; }
            set { member.name = value;  }
        }
        public string expectedLapTime 
        {
            get 
            { 
                return ViewModelHelper.ToString( member.expectedLapTime ); 
            }
            set 
            {
                TimeSpan parsedTime;
                if (ViewModelHelper.ParseTime(value, out parsedTime))
                {
                    member.expectedLapTime = parsedTime;
                    ShowMessage("");
                }
                else
                {
                    ShowMessage("Enter valid time");
                }
            }
        }

        private ViewModelHelper.ShowMessage ShowMessage; // delagate
        private ITeamMember member;
    }
}
