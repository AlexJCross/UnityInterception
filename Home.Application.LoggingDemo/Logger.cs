namespace Home.Application.LoggingDemo
{
    using System;

    public interface ILogger
    {
        void Log(string message);

        void Log(string format, params object[] arg);
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string format, params object[] arg)
        {
            Log(string.Format(format, arg));
        }
    }
}
