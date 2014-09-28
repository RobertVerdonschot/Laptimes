using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using LapTimes.Builder;
using LapTimes.Logic;

namespace LapTimes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Controller controller;

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            base.OnStartup(e);

            IOC.Setup(new ProductionSetup());

            controller = IOC.Get<Controller>();
        }

        protected override void OnExit(System.Windows.ExitEventArgs e)
        {
            base.OnExit(e);

            IOC.Teardown();
        }
    }
}
