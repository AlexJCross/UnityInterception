namespace Home.Application.LoggingDemo
{
    using System;

    public interface IMarioKartSkillLevelService
    {
        int GetSkillLevel(Player player);
    }

    public class MarioKartSkillLevelService : IMarioKartSkillLevelService
    {
        public MarioKartSkillLevelService()
        {
            // Default
        }

        public int GetSkillLevel(Player player)
        {
            switch (player)
            {
                case Player.AC:
                    return 96;
                case Player.CTA:
                    return 34;
                case Player.RN:
                    return 91;
                default:
                    throw new NotSupportedException(
                        string.Format("The player {0} is not supported.", player));
            }
        }
    }

    public class LoggingMarioKartSkillLevelService : IMarioKartSkillLevelService
    {
        private readonly IMarioKartSkillLevelService service;
        private readonly ILogger logger;

        public LoggingMarioKartSkillLevelService(IMarioKartSkillLevelService service, ILogger logger)
        {
            this.logger = logger;
            this.service = service;
        }

        public int GetSkillLevel(Player player)
        {
            this.logger.Log("Before method call with parameter " + player);

            var level = this.service.GetSkillLevel(player);

            this.logger.Log("Exited with value: " + level);

            return level;
        }
    }

    public class CachingMarioKartSkillLevelService : IMarioKartSkillLevelService
    {
        private readonly IMarioKartSkillLevelService service;

        public CachingMarioKartSkillLevelService(IMarioKartSkillLevelService service)
        {
            this.service = service;
        }

        public int GetSkillLevel(Player player)
        {
            if (this.IsCached)
            {
                return this.RetrieveFromCache();
            }

            return this.service.GetSkillLevel(player);
        }

        private bool IsCached
        {
            get { return false; }
        }

        private int RetrieveFromCache()
        {
            return default(int);
        }
    }
}