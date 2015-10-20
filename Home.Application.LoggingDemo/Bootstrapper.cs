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
            Console.WriteLine("Press any key to continue...");
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
                          .RegisterInstance<ILogger>(new Logger());

            this.Container.RegisterType<IMarioKartSkillLevelService, MarioKartSkillLevelService>("Root");
            this.Container.RegisterType<IMarioKartSkillLevelService, CachingMarioKartSkillLevelService>(
                "Caching", new InjectionConstructor(new ResolvedParameter<IMarioKartSkillLevelService>("Root")));

            this.Container.RegisterType<IMarioKartSkillLevelService, LoggingMarioKartSkillLevelService>(
                new InjectionConstructor(
                    new ResolvedParameter<IMarioKartSkillLevelService>("Caching"),
                    new ResolvedParameter<ILogger>()));

        }
    }

    internal sealed class InterceptorBootstrapper : Bootstrapper
    {
        protected override void ConfigureContainer()
        {
            this.Container.RegisterType<MainApp, MainApp>()
                          .RegisterInstance<ILogger>(new Logger());

            this.Container.AddNewExtension<Interception>();
            
            this.Container.RegisterType<IMarioKartSkillLevelService, MarioKartSkillLevelService>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>());
        }
    }
}