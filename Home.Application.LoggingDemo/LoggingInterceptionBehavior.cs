namespace Home.Application.LoggingDemo
{
    using Microsoft.Practices.Unity.InterceptionExtension;
    using System;
    using System.Collections.Generic;

    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        private readonly ILogger logger;

        public LoggingInterceptionBehavior(ILogger logger)
        {
            this.logger = logger;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.Name != "GetPosition")
            {
                return getNext()(input, getNext);
            }

            // Before invoking the method on the original target.
            this.logger.Log(String.Format(
                "{0}: Invoking method {1}",
                DateTime.Now.ToLongTimeString(),
                input.MethodBase.Name));
    
            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);
    
            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                this.logger.Log(String.Format(
                    "{0}: Method {1} threw exception {2}",
                    DateTime.Now.ToLongTimeString(),
                    input.MethodBase.Name,
                    result.Exception.Message));
            }
            else
            {
                this.logger.Log(String.Format(
                    "{0}: Method {1} returned {2}",
                    DateTime.Now.ToLongTimeString(),
                    input.MethodBase.Name,
                    result.ReturnValue ?? "void"));
            }
    
            return result;
        }

        public bool WillExecute
        {
            get
            {
                return true;
            }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        
        private void WriteLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}
