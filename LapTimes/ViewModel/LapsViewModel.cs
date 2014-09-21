using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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

        public ObservableCollection<LapViewModel> LapsRunning
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
            _race = new Race();
            _race.raceName = "TestRace";
            _race.laps.Add(new Lap(1, new TeamMember("Piet"), new DateTime(2014, 8, 24, 13, 00, 0), true, true));
            _race.laps.Add(new Lap(2, new TeamMember("Jaap"), new DateTime(2014, 8, 24, 13, 21, 0), true, true));
            _race.laps.Add(new Lap(3, new TeamMember("Kees"), new DateTime(2014, 8, 24, 13, 42, 0), true, true));
            _race.laps.Add(new Lap(4, new TeamMember("Hans"), new DateTime(2014, 8, 24, 14, 03, 0), true, true));
            _race.laps.Add(new Lap(5, new TeamMember("Piet"), new DateTime(2014, 8, 24, 14, 24, 0), true, false));
            _race.laps.Add(new Lap(6, new TeamMember("Jaap"), new DateTime(2014, 8, 24, 14, 45, 0), false, false));
            _race.laps.Add(new Lap(7, new TeamMember("Kees"), new DateTime(2014, 8, 24, 15, 06, 0), false, false));
            _race.laps.Add(new Lap(8, new TeamMember("Hans"), new DateTime(2014, 8, 24, 15, 27, 0), false, false));
                        
        }
    }
}

