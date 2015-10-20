namespace Home.Application.LoggingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // var bootstrapper = new DecoratorBootstrapper();
            var bootstrapper = new InterceptorBootstrapper();
            bootstrapper.Run();
        }
    }
}