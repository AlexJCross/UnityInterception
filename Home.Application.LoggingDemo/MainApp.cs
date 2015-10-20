namespace Home.Application.LoggingDemo
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class MainApp
    {
        private readonly IMarioKartPositionService positionService;
        private readonly IItemFactory itemFactory;
        private readonly ILogger logger;
    
        public MainApp(IMarioKartPositionService positionService, IItemFactory factory, ILogger logger)
        {
            this.itemFactory = factory;
            this.positionService = positionService;
            this.logger = logger;
        }
    
        public void Run()
        {
            // Create a player
            IPlayer player = new Player { Name = PlayerName.CTA };
            
            // Log using behaviour interception
            Position position = this.positionService.GetPosition(player.Name);
            
            // Log using the Logging attribute
            Item powerUp = this.itemFactory.Create(position);
    
            // Log by dynamically generating a proxy
            IPlayer proxyPlayer = Intercept.ThroughProxy<IPlayer>(
                player, 
                new InterfaceInterceptor(),
                new IInterceptionBehavior[] { new LoggingInterceptionBehavior(this.logger) });
    
            proxyPlayer.FireItem(powerUp);
        }
    }
}