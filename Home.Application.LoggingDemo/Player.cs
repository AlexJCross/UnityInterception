namespace Home.Application.LoggingDemo
{
    using System;

    public class Player : IPlayer
    {
        public PlayerName Name { get; set; }
    
        public void FireItem(Item item)
        {
            Console.WriteLine(
                "{0} fired a {1} {2}",
                this.Name,
                item.Type,
                this.Name == PlayerName.CTA ? "but missed" : "and hit");
        }
    }

    public interface IPlayer
    {
        PlayerName Name { get; set; }

        void FireItem(Item item);
    }

    public enum PlayerName
    {
        None = 0,
        AC,
        CTA,
        RN
    }
}
