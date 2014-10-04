using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;

using LapTimes.Model;
using LapTimes.Logic;

namespace LapTimes.Builder
{
    class ProductionSetup : NinjectModule
    {
        public override void Load()
        {
            // Logic
            Bind<IController>().To<Controller>().InSingletonScope();
            
            // Model
            Bind<IRace>().To<Race>().InSingletonScope();
            Bind<IRaceSetup>().To<RaceSetup>();

            // Bind<IConfigurationDataStorage>().To<ConfigurationDataStorage>().InSingletonScope();
            // Bind<UIModel>().ToSelf().InSingletonScope();
        }
    }
}
