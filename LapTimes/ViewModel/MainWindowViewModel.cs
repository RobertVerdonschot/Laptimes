using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MicroMvvm;

namespace LapTimes.ViewModel
{
    enum Screen
    {
        Laps,
        Setup,
        Analyse
    }    

    class MainWindowViewModel : ObservableObject
    {
        /* property ActiveScreen */
        public const string strActiveScreen = "ActiveScreen";
        private Screen _activeScreen;        
        public Screen ActiveScreen
        {
            get { return _activeScreen; }
            set { 
                _activeScreen = value; 
                RaisePropertyChanged(strActiveScreen); 
            }
        }

        /* ICommand NavigateToSetup  */
        void NavigateToSetupExecute()
        {
            ActiveScreen = Screen.Setup;
        }

        bool CanNavigateToSetupExecute()
        {
            return ActiveScreen == Screen.Laps;
        }

        public ICommand NavigateToSetup { get { return new RelayCommand(NavigateToSetupExecute, CanNavigateToSetupExecute); } }

        /* ICommand NavigateToAnalyse  */
        void NavigateToAnalyseExecute()
        {
            ActiveScreen = Screen.Analyse;
        }

        bool CanNavigateToAnalyseExecute()
        {
            return ActiveScreen == Screen.Laps;
        }

        public ICommand NavigateToAnalyse { get { return new RelayCommand(NavigateToAnalyseExecute, CanNavigateToAnalyseExecute); } }

        /* ICommand NavigateBack  */
        void NavigateBackExecute()
        {
            ActiveScreen = Screen.Laps;
        }

        bool CanNavigateBackExecute()
        {
            return ActiveScreen != Screen.Laps;
        }

        public ICommand NavigateBack { get { return new RelayCommand(NavigateBackExecute, CanNavigateBackExecute); } }

    }
}
