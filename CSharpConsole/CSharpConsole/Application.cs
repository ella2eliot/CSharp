using HealthCareConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    public class Application : IApplication
    {
        public IBusinessLogic BusinessLogic { get; set; }

        public Application(IBusinessLogic businessLogic)
        {
            BusinessLogic = businessLogic;
        }
        public void Run()
        {
            BusinessLogic.ProcessData();
        }
    }
}
