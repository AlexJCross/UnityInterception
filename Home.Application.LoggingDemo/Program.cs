namespace Home.Application.LoggingDemo
{
    using Microsoft.Practices.Unity;
    using Prism.Unity;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            // var bootstrapper = new DecoratorBootstrapper();
            var bootstrapper = new InterceptorBootstrapper();
            bootstrapper.Run();
        }
    }

    

    public class MainApp
    {
        private readonly  IMarioKartSkillLevelService skillLevelService;

        public MainApp(IMarioKartSkillLevelService skilllevelService)
        {
            this.skillLevelService = skilllevelService;
        }

        public void Run()
        {
            var skillLevel = this.skillLevelService.GetSkillLevel(Player.RN);
        }
    }

    public enum Player
    {
        None = 0,
        AC,
        CTA,
        RN
    }

    
}