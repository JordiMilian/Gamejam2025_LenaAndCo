public abstract class Prize
{
    private int prizeValue;

    public int Value
    {
        get { return prizeValue; }
        protected set { prizeValue = value; }
    }

    protected Prize(int value)
    {
        Value = value;
    }
}

public class FishPrize : Prize
{
    public FishPrize(int healthValue) : base(healthValue) { }
}

public class CoinPrize : Prize
{
    public CoinPrize(int coinValue) : base(coinValue) { }
}
