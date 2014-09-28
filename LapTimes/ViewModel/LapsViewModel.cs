using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MicroMvvm;
using LapTimes.Builder;
using LapTimes.Logic;
using LapTimes.Model;

namespace LapTimes.ViewModel
{
    class LapsViewModel : ObservableObject
    {
        public LapsViewModel()
        {
            _race = IOC.Get<IRace>();
            _race.LapsChangedEvent += LapsChangedEventHandler;
        }

        public ObservableCollection<LapViewModel> LapsToDo
        {
            get
            {
                return LapsToLapViewmodels(_race.laps, false, false);
            }
            private set { }
        }

        public ObservableCollection<LapViewModel> LapsInProgress
        {
            get
            {
                return LapsToLapViewmodels(_race.laps, true, false);
            }
            private set { }
        }

        public ObservableCollection<LapViewModel> LapsDone
        {
            get
            {
                return LapsToLapViewmodels(_race.laps, true, true);
            }
            private set { }
        }

        public void LapsChangedEventHandler(object sender, ILap e)
        {
            RaisePropertyChanged("LapsToDo");
            RaisePropertyChanged("LapsInProgress");
            RaisePropertyChanged("LapsDone");
        }

        public void HandOverExecute()
        {
            // Bussiness logic. to be moved...
            bool startnext = false;
            foreach (ILap lap in _race.laps)
            {
                bool startcurrent = startnext;

                if (lap.started && !lap.finished)
                {
                    lap.finished = true;
                    startnext = true;
                }
                else
                {
                    startnext = false;
                }

                if (startcurrent)
                {
                    lap.started = true;
                }
            }
        }

        public bool CanHandOverExecute()
        {
            return true;
        }

        public ICommand HandOver { get { return new RelayCommand(HandOverExecute, CanHandOverExecute); } }


        // Convert Lap List to LapViewmodel Collection, 
        // query only the laps that have the started and finished flags set correctly
        private ObservableCollection<LapViewModel> LapsToLapViewmodels(IList<ILap> laps, bool started, bool finished)
        {
            ObservableCollection<LapViewModel> lapVMCollection = new ObservableCollection<LapViewModel>();
            foreach (ILap lap in _race.laps)
            {
                if ((lap.started == started) && (lap.finished == finished))
                {
                    lapVMCollection.Add(new LapViewModel(lap));
                }
            }
            return lapVMCollection;
        }

        private IRace _race;
    }
}

