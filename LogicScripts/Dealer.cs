public class Dealer
{
    private int objectPrice;

    public int ObjectPrice
    {
        get { return objectPrice; }
        set { objectPrice = value; }
    }

    public Dealer(int price)
    {
        ObjectPrice = price;
    }
}
