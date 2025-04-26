public class Player
{
    private bool isSeal;
    private int coins;
    private int strength;
    private int maxLife = 12;
    private int currentLife;

    public bool IsSeal
    {
        get { return isSeal; }
        set { isSeal = value; }
    }

    public int Life
    {
        get { return currentLife; }
        set { currentLife = value; }
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int MaxLife
    {
        get { return maxLife; }
    }

    public Player()
    {
        IsSeal = false;
        Life = maxLife;
        Coins = 0;
    }
}
