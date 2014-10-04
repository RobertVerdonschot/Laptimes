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
            race = IOC.Get<IRace>();
            race.LapsChangedEvent += LapsChangedEventHandler;

            controller = IOC.Get<IController>();

            UpdateHeights();
        }

        public ObservableCollection<LapViewModel> LapsToDo
        {
            get
            {
                return LapsToLapViewmodels(race.laps, false, false);
            }
            private set { }
        }

        public ObservableCollection<LapViewModel> LapsInProgress
        {
            get
            {
                return LapsToLapViewmodels(race.laps, true, false);
            }
            private set { }
        }

        public ObservableCollection<LapViewModel> LapsDone
        {
            get
            {
                return LapsToLapViewmodels(race.laps, true, true);
            }
            private set { }
        }

        public void LapsChangedEventHandler(object sender, ILap e)
        {
            RaisePropertyChanged("LapsToDo");
            RaisePropertyChanged("LapsInProgress");
            RaisePropertyChanged("LapsDone");
            UpdateHeights();
        }


        public uint HeightDone { get; private set; }
        public uint HeightInProgress { get; private set; }
        public uint HeightToDo { get; private set; }

        private void UpdateHeights()
        {
            // constants for finetuning
            const uint maxTotalHeight = 255;
            const uint maxItemsPerList = 4;
            const uint heightHeader = 22;
            const uint heightPerItem = 21;

            uint nrDone = 0;
            uint nrInProgress = 0;
            uint nrToDo = 0;

            // Count laps todo, in progress and done
            foreach (ILap lap in race.laps)
            {
                if (lap.started)
                {
                    if (lap.finished)
                    {
                        nrDone++;
                    }
                    else
                    {
                        nrInProgress++;
                    }
                }
                else
                {
                    nrToDo++;
                }
            }

            // Calculate heights
            HeightInProgress = nrInProgress * heightPerItem + heightHeader; // always show complete in progress
            if (nrDone < nrToDo) // start with shortest list and give the other the remaining space
            {
                if (nrDone > maxItemsPerList) 
                    nrDone = maxItemsPerList;
                HeightDone = nrDone * heightPerItem + heightHeader;
                HeightToDo = maxTotalHeight - HeightInProgress - HeightDone;
            }
            else // (nrToDo < nrDone)
            {
                if (nrToDo > maxItemsPerList)
                    nrToDo = maxItemsPerList;
                HeightToDo = nrToDo * heightPerItem + heightHeader;
                HeightDone = maxTotalHeight - HeightInProgress - HeightToDo;
            }

            RaisePropertyChanged("HeightDone");
            RaisePropertyChanged("LapsInProgress");
            RaisePropertyChanged("HeightToDo");
        }


        public void HandOverExecute()
        {
            controller.HandOver();
        }

        public ICommand HandOver { get { return new RelayCommand(HandOverExecute); } }


        // Convert Lap List to LapViewmodel Collection, 
        // query only the laps that have the started and finished flags set correctly
        private ObservableCollection<LapViewModel> LapsToLapViewmodels(IList<ILap> laps, bool started, bool finished)
        {
            ObservableCollection<LapViewModel> lapVMCollection = new ObservableCollection<LapViewModel>();
            foreach (ILap lap in race.laps)
            {
                if ((lap.started == started) && (lap.finished == finished))
                {
                    lapVMCollection.Add(new LapViewModel(lap));
                }
            }
            return lapVMCollection;
        }

        private IRace race;
        private IController controller;
    }
}

