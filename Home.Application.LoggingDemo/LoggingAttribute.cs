namespace Home.Application.LoggingDemo
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using System;

    public class LoggingAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return container.Resolve<ILoggingCallHandler>();
        }
    }

    public interface ILoggingCallHandler : ICallHandler
    {
    }

    public class LoggingCallHandler : ILoggingCallHandler
    {
        private readonly ILogger logger;

        public LoggingCallHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // Before invoking the method on the original target.
            this.logger.Log(
                "{0}: Invoking method {1}",
                DateTime.Now.ToLongTimeString(),
                input.MethodBase.Name);

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                this.logger.Log(
                    "{0}: Method {1} threw exception {2}",
                    DateTime.Now.ToLongTimeString(),
                    input.MethodBase.Name,
                    result.Exception.Message);
            }
            else
            {
                this.logger.Log(
                    "{0}: Method {1} returned {2}",
                    DateTime.Now.ToLongTimeString(),
                    input.MethodBase.Name,
                    result.ReturnValue ?? "void");
            }

            return result;
        }

        public int Order { get; set; }
    }
}
