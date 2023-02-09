using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareConsole.Utilities
{
    public class DataAccess : IDataAccess
    {
        public void LoadData()
        {
            Console.WriteLine("Loading data.");
        }
        public void SaveData(string name)
        {
            Console.WriteLine($"Saving {name}");
        }
    }
}
