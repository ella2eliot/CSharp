using HealthCareConsole.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareConsole
{
    public class BusinessLogic : IBusinessLogic
    {
        public ILogger Logger { get; set; }
        public IDataAccess DataAccess { get; set; }

        public BusinessLogic(ILogger logger, IDataAccess dataAccess)
        {
            Logger=logger;
            DataAccess=dataAccess;
        }

        public void ProcessData()
        {
            Logger logger = new Logger();
            DataAccess dataAccess = new DataAccess();

            logger.Log("Start the processing of data");
            Console.WriteLine("Process the data");
            dataAccess.LoadData();
            dataAccess.SaveData("ProcessdInfo");
            logger.Log("Finished processing of the data");
        }
    }
}
