using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LapTimes.ViewModel;

namespace LapTimes.View
{
    /// <summary>
    /// Interaction logic for Screens.xaml
    /// </summary>
    public partial class Screens : UserControl
    {
        private ScreensVM screensVM;

        public Screens()
        {
            InitializeComponent();
            screensVM = (ScreensVM)this.Resources["screensVM"]; 
        }

        private void BtnSetup_Click(object sender, RoutedEventArgs e)
        {
            screensVM.SwitchToSetupScreen();
        }

        private void BtnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            screensVM.SwitchToAnalyseScreen();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            screensVM.SwitchToLapsScreen();
        }
    }
}
