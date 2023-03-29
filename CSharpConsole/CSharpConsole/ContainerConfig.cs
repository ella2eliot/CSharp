using Autofac;
using HealthCareConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            // top to down
            // key/value pair list of all of different classes
            var builder=new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();

            // load with namespace contains Utilities
            // load class interface with "I"{classname}
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(HealthCareConsole)))
                .Where(x=> x.Namespace.Contains("Utilities"))
                .As(x=> x.GetInterfaces().FirstOrDefault(i=>i.Name=="I"+x.Name));

            return builder.Build();
        }
    }
}
