using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using Ninject.Modules;

namespace LapTimes.Builder
{
    class IOC
    {
        private static IKernel kernel;

        public static void Setup(INinjectModule module)
        {
          if (kernel == null)
          {
            kernel = new StandardKernel(module);
          }
        }

        public static void Teardown()
        {
          kernel.Dispose();
          kernel = null;
        }

        public static T Get<T>()
        {
          return kernel.Get<T>();
        }

        public static T Get<T>(string namedBinding)
        {
          return kernel.Get<T>(namedBinding);
        }
      }
    
}
