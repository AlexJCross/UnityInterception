namespace Home.Application.LoggingDemo
{
    public interface IItem
    {
        ItemType Type { get; set; }
    }

    public class Item
    {
        public ItemType Type { get; set; }

        public override string ToString()
        {
            return string.Format("Item ({0})", this.Type);
        }
    }

    public enum ItemType
    {
        None = 0,
        Coin,
        Banana,
        Mushroom,
        RedShell,
        GreenShell,
        SpinyShell,
        Lightning,
        BulletBill
    }
}
