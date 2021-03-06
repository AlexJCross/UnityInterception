namespace Home.Application.LoggingDemo
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using System;

    internal abstract class Bootstrapper
    {
        protected readonly IUnityContainer Container = new UnityContainer();

        protected abstract void ConfigureContainer();

        private void LaunchApp()
        {
            Container.Resolve<MainApp>().Run();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void Run()
        {
            this.ConfigureContainer();
            this.LaunchApp();
        }
    }

    internal sealed class DecoratorBootstrapper : Bootstrapper
    {
        protected override void ConfigureContainer()
        {
            this.Container.RegisterType<MainApp, MainApp>()
                          .RegisterInstance<ILogger>(new Logger())
                          .RegisterInstance<IItemFactory>(new ItemFactory());

            this.Container.RegisterType<IMarioKartPositionService, MarioKartPositionService>("Root");
            this.Container.RegisterType<IMarioKartPositionService, CachingMarioKartPositionService>(
                "Caching", new InjectionConstructor(new ResolvedParameter<IMarioKartPositionService>("Root")));

            this.Container.RegisterType<IMarioKartPositionService, LoggingMarioKartPositionService>(
                new InjectionConstructor(
                    new ResolvedParameter<IMarioKartPositionService>("Caching"),
                    new ResolvedParameter<ILogger>()));

        }
    }

    internal sealed class InterceptorBootstrapper : Bootstrapper
    {
        protected override void ConfigureContainer()
        {
            this.Container.RegisterType<MainApp, MainApp>()
                          .RegisterType<ILoggingCallHandler, LoggingCallHandler>(new ContainerControlledLifetimeManager())
                          .RegisterInstance<ILogger>(new Logger());

            // Add interception functionality
            this.Container.AddNewExtension<Interception>();

            // Register behaviour style logging
            this.Container.RegisterType<IMarioKartPositionService, MarioKartPositionService>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>());

            // Register Policy injection style logging
            this.Container.RegisterType<IItemFactory, ItemFactory>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<PolicyInjectionBehavior>());

            this.Container
                .Configure<Interception>()
                .AddPolicy("Logging")
                .AddMatchingRule<AssemblyMatchingRule>(new InjectionConstructor(new InjectionParameter("Home.Application.LoggingDemo")))
                .AddCallHandler<LoggingCallHandler>(new ContainerControlledLifetimeManager());
        }
    }
}