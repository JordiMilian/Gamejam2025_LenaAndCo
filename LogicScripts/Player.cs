public class Player
{
    private bool isSeal;
    private int life;
    private int coins;
    private int strength;
    private int maxLife = 12;

    public bool IsSeal
    {
        get { return isSeal; }
        set { isSeal = value; }
    }

    public int Life
    {
        get { return life; }
        set { life = value; }
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

    public Player(int life)
    {
        IsSeal = false;
        Life = life;
        Coins = 0;
    }
}
