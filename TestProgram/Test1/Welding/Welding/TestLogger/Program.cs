using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace TestLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger.Debug("This is debug test info.");

            var logger = LogManager.GetLogger("ConsoleLogger");

            logger.Error("xxx");

            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
    }
}
