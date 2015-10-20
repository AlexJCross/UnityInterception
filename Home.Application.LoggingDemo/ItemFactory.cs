namespace Home.Application.LoggingDemo
{
    using System;

    [Logging]
    public interface IItemFactory
    {
        Item Create(Position position);
    }

    public class ItemFactory : IItemFactory
    {
        public Item Create(Position position)
        {
            switch (position)
            {
                case Position.First:
                    return new Item { Type = ItemType.RedShell };
                case Position.CloseSecond:
                    return new Item { Type = ItemType.GreenShell };
                case Position.Eighth:
                    return new Item { Type = ItemType.SpinyShell };
                default:
                    throw new NotSupportedException("Position not supported");
            }
        }
    }
}
