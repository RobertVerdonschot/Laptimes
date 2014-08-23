using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.ViewModel
{
    //using Model;
    using System.ComponentModel;
    
    /// <summary>
    /// Contains state and logic for switching between the screens
    /// </summary>
    public class ScreensVM : INotifyPropertyChanged
    {
        private bool _lapsScreenActive = false;
        private bool _setupScreenActive = false;
        private bool _analysisScreenActive = false;

        public bool LapsScreenActive 
        { 
            get { return _lapsScreenActive; } 
            set { _lapsScreenActive = value; } 
        }
        public bool SetupScreenActive
        { 
            get { return _setupScreenActive; } 
            set { _setupScreenActive = value; } 
        }
        public bool AnalysisScreenActive
        { 
            get { return _analysisScreenActive; } 
            set { _analysisScreenActive = value; } 
        }

        public ScreensVM()
        {
            SwitchToLapsScreen();
        }

        public void SwitchToLapsScreen()
        {
            SwitchToScreen(out _lapsScreenActive);
        }

        public void SwitchToSetupScreen()
        {
            SwitchToScreen(out _setupScreenActive);
        }

        public void SwitchToAnalyseScreen()
        {
            SwitchToScreen(out _analysisScreenActive);
        }

        private void SwitchToScreen(out bool screen)
        {
            // Reset all screens
            _lapsScreenActive = false;
            _setupScreenActive = false;
            _analysisScreenActive = false;

            // Set requested screen active
            screen = true;

            // Notify all screens changed
            OnPropertyChanged("LapsScreenActive");
            OnPropertyChanged("SetupScreenActive");
            OnPropertyChanged("AnalysisScreenActive");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
