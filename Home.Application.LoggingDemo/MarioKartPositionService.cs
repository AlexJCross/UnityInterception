namespace Home.Application.LoggingDemo
{
    using System;

    public interface IMarioKartPositionService
    {
        Position GetPosition(PlayerName player);

        void Overtake(PlayerName player);
    }

    public enum Position
    {
        None = 0,
        First,
        CloseSecond,
        Eighth
    }

    public class MarioKartPositionService : IMarioKartPositionService
    {
        public MarioKartPositionService()
        {
            // Default
        }

        public Position GetPosition(PlayerName player)
        {
            switch (player)
            {
                case PlayerName.AC:
                    return Position.First;
                case PlayerName.CTA:
                    return Position.Eighth;
                case PlayerName.RN:
                    return Position.CloseSecond;
                default:
                    throw new NotSupportedException(
                        string.Format("The player {0} is not supported.", player));
            }
        }


        public void Overtake(PlayerName player)
        {
            if (player != PlayerName.AC )
            {
                Console.WriteLine("{0} overtook Luigi", player.ToString());
            }
        }
    }

    public class LoggingMarioKartPositionService : IMarioKartPositionService
    {
        private readonly IMarioKartPositionService service;
        private readonly ILogger logger;

        public LoggingMarioKartPositionService(IMarioKartPositionService service, ILogger logger)
        {
            this.logger = logger;
            this.service = service;
        }

        public Position GetPosition(PlayerName player)
        {
            this.logger.Log("Before method call with parameter " + player);

            var position = this.service.GetPosition(player);

            this.logger.Log("Exited with value: " + position);

            return position;
        }


        public void Overtake(PlayerName player)
        {
            this.service.Overtake(player);
        }
    }

    public class CachingMarioKartPositionService : IMarioKartPositionService
    {
        private readonly IMarioKartPositionService service;

        public CachingMarioKartPositionService(IMarioKartPositionService service)
        {
            this.service = service;
        }

        public Position GetPosition(PlayerName player)
        {
            if (this.IsCached)
            {
                return this.RetrieveFromCache();
            }

            return this.service.GetPosition(player);
        }

        private bool IsCached
        {
            get { return false; }
        }

        private Position RetrieveFromCache()
        {
            return default(Position);
        }

        public void Overtake(PlayerName player)
        {
            this.service.Overtake(player);
        }
    }
}