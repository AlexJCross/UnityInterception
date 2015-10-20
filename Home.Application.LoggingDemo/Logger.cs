using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Home.Application.LoggingDemo
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
