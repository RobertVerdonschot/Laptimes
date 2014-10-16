using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicroMvvm;
using LapTimes.Builder;
using LapTimes.Logic;
using LapTimes.Model;

namespace LapTimes.ViewModel
{
    class SetupViewModel : ObservableObject
    {
        public SetupViewModel()
        {
            setup = IOC.Get<IRace>().setup;
            setup.SetupChangedEvent += SetupChangedEventHandler;
            controller = IOC.Get<IController>();
        }

        string _message;
        public string message { 
            get { return _message; }
            set 
            { 
                _message = value; 
                RaisePropertyChanged(() => message); 
            } 
        }

        public uint lapDistance 
        {
            get { return setup.lapDistance; } 
            set { setup.lapDistance = value;  } 
        }
        public string raceDuration 
        { 
            get 
            { 
                return ViewModelHelper.ToString(setup.raceDuration); 
            } 
            set 
            { 
                TimeSpan newValue;
                if (ViewModelHelper.ParseTime(value, out newValue))
                {
                    setup.raceDuration = newValue;
                }
            } 
        }

        public string startTime
        {
            get 
            { 
                return ViewModelHelper.ToString(setup.startTime.TimeOfDay); 
            }
            set
            {
                TimeSpan newValue;
                if (ViewModelHelper.ParseTime(value, out newValue))
                {
                    setup.startTime = setup.startTime.Date + newValue;
                    ShowMessage("");
                }
                else
                {
                    ShowMessage("Enter valid time");
                }

            } 
        }
        public String startDate
        {
            get { return setup.startTime.Date.ToShortDateString(); }
            set
            {
                DateTime newValue;
                if (DateTime.TryParse(value, out newValue))
                {
                    setup.startTime = newValue + setup.startTime.TimeOfDay;
                    ShowMessage("");
                }
                else
                {
                    ShowMessage("Enter valid date");
                }
            }
        }
        public uint lapsPerShift 
        { 
            get { return setup.lapsPerShift; } 
            set { setup.lapsPerShift = value; } 
        }
        public ObservableCollection<MemberViewModel> team
        {
            get 
            {
                ObservableCollection<MemberViewModel> memberViewmodelList = new ObservableCollection<MemberViewModel>(); 
                foreach (ITeamMember member in setup.team)
                {
                    memberViewmodelList.Add(new MemberViewModel(member, ShowMessage));
                }
                return memberViewmodelList;
            }
            set { team = value;  }
        }

        void GenerateLapsExecute()
        {
            controller.GenerateLaps();            
        }

        public ICommand GenerateLaps { get { return new RelayCommand(GenerateLapsExecute); } }

        private void SetupChangedEventHandler(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => lapDistance);
            RaisePropertyChanged(() => raceDuration);
            RaisePropertyChanged(() => startTime);
            RaisePropertyChanged(() => lapsPerShift);
            RaisePropertyChanged(() => team);
        }

        private void ShowMessage(string message)
        {
            this.message = message;
        }

        private ISetup setup;
        private IController controller;
    }
}
