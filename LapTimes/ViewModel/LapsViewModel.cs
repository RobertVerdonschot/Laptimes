using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MicroMvvm;
using LapTimes.Model;

namespace LapTimes.ViewModel
{
    class LapsViewModel : ObservableObject
    {
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

        // Convert Lap List to LapViewmodel Collection, 
        // query only the laps that have the started and finished flags set correctly
        private ObservableCollection<LapViewModel> LapsToLapViewmodels(IList<ILap> laps, bool started, bool finished)
        {
            ObservableCollection<LapViewModel> lapVMCollection = new ObservableCollection<LapViewModel>();
            foreach(ILap lap in _race.laps)
            {
                if ((lap.started == started) && (lap.finished == finished))
                {
                    lapVMCollection.Add(new LapViewModel(lap));
                }
            }
            return lapVMCollection;
        }

        // Temporary for testing: hold an instance of Race here
        private IRace _race;

        public LapsViewModel()
        {
            // Temporary for testing: instantiate the race and create some laps
            Race race = new Race();
            race.raceName = "TestRace";
            race.laps.Add(new Lap(race, 1, new TeamMember("Piet"), new DateTime(2014, 8, 24, 13, 00, 0), true, true));
            race.laps.Add(new Lap(race, 2, new TeamMember("Jaap"), new DateTime(2014, 8, 24, 13, 21, 0), true, true));
            race.laps.Add(new Lap(race, 3, new TeamMember("Kees"), new DateTime(2014, 8, 24, 13, 42, 0), true, true));
            race.laps.Add(new Lap(race, 4, new TeamMember("Hans"), new DateTime(2014, 8, 24, 14, 03, 0), true, true));
            race.laps.Add(new Lap(race, 5, new TeamMember("Piet"), new DateTime(2014, 8, 24, 14, 24, 0), true, false));
            race.laps.Add(new Lap(race, 6, new TeamMember("Jaap"), new DateTime(2014, 8, 24, 14, 45, 0), false, false));
            race.laps.Add(new Lap(race, 7, new TeamMember("Kees"), new DateTime(2014, 8, 24, 15, 06, 0), false, false));
            race.laps.Add(new Lap(race, 8, new TeamMember("Hans"), new DateTime(2014, 8, 24, 15, 27, 0), false, false));
            race.LapsChangedEvent += LapsChangedEventHandler;
            _race = race;            
        }

        void LapsChangedEventHandler(object sender, ILap e)
        {
            RaisePropertyChanged("LapsToDo");
            RaisePropertyChanged("LapsInProgress");
            RaisePropertyChanged("LapsDone");
        }

        void HandOverExecute()
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

        bool CanHandOverExecute()
        {
            return true;
        }

        public ICommand HandOver { get { return new RelayCommand(HandOverExecute, CanHandOverExecute); } }

    }
}

